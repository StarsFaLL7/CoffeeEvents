using System.Net;
using System.Net.Mime;
using System.Security.Authentication;
using System.Text;
using ClientApp.Services.Auth;
using ClientApp.Services.Http.Models;
using CoffeeEvents.Controllers.Authorization.Responses;
using Domain;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using ContentType = ClientApp.Services.Http.Enums.ContentType;

namespace ClientApp.Services.Http;

public class HttpService
{
    private readonly string _host;
    private readonly int _port;
    private readonly TokenAuthManager _tokenAuthManager;
    private readonly Dictionary<string, string> _defaultHeaders;

    public HttpService(string host, int port, Dictionary<string, string>? defaultHeaders, TokenAuthManager tokenAuthManager)
    {
        _host = host;
        _port = port;
        _tokenAuthManager = tokenAuthManager;
        _defaultHeaders = defaultHeaders ?? new Dictionary<string, string>();
    }
    
    public async Task<T?> SendHttpRequestAsync<T>(HttpRequestData requestData)
    {
        var token = await _tokenAuthManager.TryGetAccessToken();
        var handler = new HttpClientHandler
        {
            CookieContainer = new CookieContainer(),
            UseCookies = true
        };
        using (var httpClient = new HttpClient(handler))
        {
            foreach (var header in _defaultHeaders)
            {
                httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }

            if (token != null)
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
            
            var uri = requestData.UseHttps ? "https://" : "http://";
            uri += $"{_host}:{_port}/{requestData.EndpointPath}";
            
            var request = new HttpRequestMessage
            {
                Method = requestData.Method,
                RequestUri = GetUriWithQuery(new Uri(uri), requestData.QueryParameterList)
            };
            
            if (requestData.Body != null)
            {
                request.Content = PrepairContent(requestData.Body, requestData.ContentType);
            }
            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"> HTTP request {request.Method.Method} {request.RequestUri} failed with response code {response.StatusCode}.");
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                if (await TryRefreshToken(requestData))
                {
                    return await SendHttpRequestAsync<T>(requestData);
                }
                await _tokenAuthManager.LogoutUserAsync();
                return default;
            }
            var contentString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            
            
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var result = JsonConvert.DeserializeObject<T>(contentString, settings);
            Console.WriteLine($"> HTTP request {request.Method.Method} {request.RequestUri} sent successfully with response code {response.StatusCode}.");
            await TryToWriteRefreshTokenToAuthManager(handler, requestData);
            return result;
        }
    }

    public async Task TryToWriteRefreshTokenToAuthManager(HttpClientHandler handler, HttpRequestData requestData)
    {
        var usriStr = (requestData.UseHttps ? "https://" : "http://") + $"{_host}:{_port}";
        var uri = new Uri(usriStr);
        var cookies = handler.CookieContainer.GetCookies(uri);
        var refreshToken = cookies[AuthOptions.RefreshTokenCookieName]?.Value;
        if (!string.IsNullOrWhiteSpace(refreshToken))
        {
            await _tokenAuthManager.SetRefreshToken(refreshToken);
        }
    }
    
    private async Task<bool> TryRefreshToken(HttpRequestData requestData)
    {
        var handler = new HttpClientHandler
        {
            CookieContainer = new CookieContainer(),
            UseCookies = true
        };
        var refreshToken = await _tokenAuthManager.GetRefreshToken();
        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            return false;
        }
        handler.CookieContainer.Add(new Cookie(AuthOptions.RefreshTokenCookieName, refreshToken, "/", _host));
        using (var httpClient = new HttpClient(handler))
        {
            var uri = requestData.UseHttps ? "https://" : "http://";
            uri += $"{_host}:{_port}/api/auth/refresh";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(uri)
            };
            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"> HTTP request to {request.RequestUri} failed with response code {response.StatusCode}.");
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return false;
            }
            response.EnsureSuccessStatusCode();
            
            var contentString = await response.Content.ReadAsStringAsync();
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var result = JsonConvert.DeserializeObject<RefreshResponse>(contentString, settings)!;
            Console.WriteLine($"> HTTP request to {request.RequestUri} sent successfully with response code {response.StatusCode}.");
            await _tokenAuthManager.LoginUserAsync(result.AccessToken);
            await TryToWriteRefreshTokenToAuthManager(handler, requestData);
            return true;
        }
    }
    
    private static Uri GetUriWithQuery(Uri uri, ICollection<KeyValuePair<string, string>> queryParameterList)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(uri);
        var isFirstQuery = true;
        foreach (var queryKvPair in queryParameterList)
        {
            if (isFirstQuery)
            {
                stringBuilder.Append('?');
                isFirstQuery = false;
            }
            else
            {
                stringBuilder.Append('&');
            }
            stringBuilder.Append(queryKvPair.Key);
            stringBuilder.Append('=');
            stringBuilder.Append(queryKvPair.Value);
        }

        return new Uri(stringBuilder.ToString());
    }
    
    private static HttpContent PrepairContent(object body, ContentType contentType)
    {
        switch (contentType)
        {
            case ContentType.ApplicationJson:
            {
                if (body is string stringBody)
                {
                    body = JToken.Parse(stringBody);
                }

                var serializeSettings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                };
                var serializedBody = JsonConvert.SerializeObject(body, serializeSettings);
                var content = new StringContent(serializedBody, Encoding.UTF8, MediaTypeNames.Application.Json);
                return content;
            }

            case ContentType.XWwwFormUrlEncoded:
            {
                if (body is not IEnumerable<KeyValuePair<string, string>> list)
                {
                    throw new Exception(
                        $"Body for content type {contentType} must be {typeof(IEnumerable<KeyValuePair<string, string>>).Name}");
                }

                return new FormUrlEncodedContent(list);
            }
            case ContentType.ApplicationXml:
            {
                if (body is not string s)
                {
                    throw new Exception($"Body for content type {contentType} must be XML string");
                }

                return new StringContent(s, Encoding.UTF8, MediaTypeNames.Application.Xml);
            }
            case ContentType.Binary:
            {
                if (body.GetType() != typeof(byte[]))
                {
                    throw new Exception($"Body for content type {contentType} must be {typeof(byte[]).Name}");
                }

                return new ByteArrayContent((byte[])body);
            }
            case ContentType.TextXml:
            {
                if (body is not string s)
                {
                    throw new Exception($"Body for content type {contentType} must be XML string");
                }

                return new StringContent(s, Encoding.UTF8, MediaTypeNames.Text.Xml);
            }
            case ContentType.MultipartFormData:
                return (MultipartFormDataContent)body;
            default:
                throw new ArgumentOutOfRangeException(nameof(contentType), contentType, null);
        }
    }
}