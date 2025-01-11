using Application.AuthService;
using Application.Cities;
using Application.Services;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Application;

public static class ApplicationStartup
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.TryAddScoped<IAuthService, AuthService.AuthService>();
        
        services.TryAddScoped<BaseService<DynamicFieldType>>();
        services.TryAddScoped<BaseService<EntryFieldValue>>();
        services.TryAddScoped<BaseService<EventSignupEntry>>();
        services.TryAddScoped<BaseService<EventSignupForm>>();
        services.TryAddScoped<BaseService<EventSignupWindow>>();
        services.TryAddScoped<BaseService<FormDynamicField>>();
        services.TryAddScoped<BaseService<OrganizerContacts>>();
        services.TryAddScoped<BaseService<User>>();
        services.TryAddScoped<BaseService<UserEvent>>();
        services.TryAddScoped<BaseService<UserRole>>();
        
        services.TryAddScoped<UserInfoService>();
        services.TryAddScoped<EventBannerImageService>();
        services.TryAddScoped<EventSignupService>();
        
        services.TryAddSingleton<ICitiesService, CitiesJsonService>();
        return services;
    }
}