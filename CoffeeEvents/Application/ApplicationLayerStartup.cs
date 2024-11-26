using Application.Services;
using Application.Services.Email;
using Application.Services.Email.Interfaces;
using Application.Services.UserProfile;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Application;

public static class ApplicationLayerStartup
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.TryAddScoped<BaseService<DynamicFieldType>>();
        services.TryAddScoped<BaseService<EntryFieldValue>>();
        services.TryAddScoped<BaseService<EventSignupEntry>>();
        services.TryAddScoped<BaseService<EventSignupForm>>();
        services.TryAddScoped<BaseService<FormDynamicField>>();
        services.TryAddScoped<BaseService<OrganizerContacts>>();
        services.TryAddScoped<BaseService<UserEvent>>();
        
        services.TryAddScoped<IUserProfileService, UserProfileService>();
        // services.TryAddScoped<BaseService<ApplicationUser>>();
        
        services.AddSingleton(new SmtpConnectionSettings
        {
            SmtpServer = "",
            AuthCredentials = null,
            Port = 587,
            EnableSsl = true
        });
        services.AddSingleton<IApplicationEmailSender, ApplicationEmailSender>();
        
        return services;
    }  
}