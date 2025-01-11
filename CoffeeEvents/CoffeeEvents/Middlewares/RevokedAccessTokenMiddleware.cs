using System.IdentityModel.Tokens.Jwt;
using Domain;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CoffeeEvents.Middlewares;

public class RevokedAccessTokenMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public RevokedAccessTokenMiddleware(RequestDelegate next, IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _next = next;
        _dbContextFactory = dbContextFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // TODO Временно отключено
        /*var dbContext = await _dbContextFactory.CreateDbContextAsync();
        var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
        if (!string.IsNullOrEmpty(token))
        {
            if (!TryGetIdFromToken(token, out var tokenId))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Access token is invalid.");
                return;
            }
            var isRevoked = await dbContext.RevokedAccessTokens.AnyAsync(t => t.Jti == tokenId);
            if (isRevoked)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Access token has been revoked.");
                return;
            }
        }*/

        await _next(context);
    }
    
    private bool TryGetIdFromToken(string token, out Guid? tokenId)
    {
        var handler = new JwtSecurityTokenHandler();
        tokenId = null;
        if (handler.CanReadToken(token))
        {
            var jwtToken = handler.ReadJwtToken(token);
            var jtiClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == AuthOptions.ClaimTypeJti);
            if (jtiClaim == null)
            {
                return false;
            }

            tokenId = new Guid(jtiClaim.Value);
            return true;
        }
        return false;
    }
}