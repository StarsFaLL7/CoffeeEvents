using Blazored.LocalStorage;
using ClientApp.Components;
using ClientApp.Services;
using ClientApp.Services.Auth;
using ClientApp.Services.Http;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthenticationStateProvider>();
builder.Services.AddScoped<TokenAuthManager>();
builder.Services.AddScoped<HttpService>(s => new HttpService("localhost", 8050, [], 
    s.GetRequiredService<TokenAuthManager>()));
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddBlazoredLocalStorage();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();