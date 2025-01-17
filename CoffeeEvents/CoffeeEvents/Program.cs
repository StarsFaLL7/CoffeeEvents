using Application;
using CoffeeEvents.Controllers.Base;
using CoffeeEvents.Utility;
using Infrastructure;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(AuthorizationConfiguration.ConfigureSwaggerWithJwtBearer);

builder.Services.AddControllers();

builder.Services.AddHostedService<RevokedAccessTokenCleanupService>();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddScoped<ControllerUtils>();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(AuthorizationConfiguration.ConfigureJwtBearerAuthorization);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorHttpOrigin",
        policy => policy.WithOrigins("http://localhost:5175")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowBlazorHttpOrigin");
app.UseAuthentication();
app.UseAuthorization();
// app.UseMiddleware<RevokedAccessTokenMiddleware>();
// app.UseHttpsRedirection();

app.MapControllers();
app.UseStaticFiles();

using (var scope = app.Services.CreateScope())
{
    InfrastructureStartup.CheckAndMigrateDatabase(scope);
}

app.Run();