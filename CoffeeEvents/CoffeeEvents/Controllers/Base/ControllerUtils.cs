﻿using Application.AuthService;
using Application.Services;
using Domain;
using Domain.Entities;
using UserEvent = Domain.Entities.UserEvent;
using UserRole = Domain.Entities.UserRole;

namespace CoffeeEvents.Controllers.Base;

public class ControllerUtils
{
    private readonly BaseService<User> _userService;
    private readonly IAuthService _authService;
    private readonly BaseService<UserRole> _roleService;
    private readonly BaseService<UserEvent> _eventService;

    public ControllerUtils(BaseService<User> userService, IAuthService authService,
        BaseService<UserRole> roleService, BaseService<UserEvent> eventService)
    {
        _userService = userService;
        _authService = authService;
        _roleService = roleService;
        _eventService = eventService;
    }
    
    public async Task<(bool Success, User? User, string ErrorMsg)> TryGetSelfUserAsync(HttpContext httpContext, 
        bool includeRole = false)
    {
        var idClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == AuthOptions.ClaimTypeUserId);
        if (idClaim == null)
        {
            return (false, null, "У пользователя некорректные claims.");
        }
        var user = await _userService.GetByIdOrDefaultAsync(new Guid(idClaim.Value));
        if (user == null)
        {
            await _authService.RemoveRefreshTokenAsync(new Guid(idClaim.Value));
            return (false, null, "Пользователя с таким id не существует. Unauthorized.");
        }

        if (includeRole)
        {
            var role = await _roleService.GetByIdOrDefaultAsync(user.RoleId);
            user.Role = role!;
        }
        
        return (true, user, "");
    }
    
    public async Task<(bool Success, string ErrorMsg, User? User)> CheckUserCanEditEventAsync(HttpContext httpContext, Guid eventId)
    {
        var info = await TryGetSelfUserAsync(httpContext, true);
        if (!info.Success)
        {
            return (false, info.ErrorMsg, null);
        }
        var user = info.User!;
        var userEvent = await _eventService.GetByIdOrDefaultAsync(eventId);
        if (userEvent == null)
        {
            return (false, "Событие с указанным id не было найдено.", null);
        }
        if (userEvent.CreatorUserId == user.Id || user.Role.CanEditOthersEvents || user.Role.IsAdmin)
        {
            return (true, "", user);
        }
        return (false, "Вы не можете редактировать данное событие.", null);
    }
    
    public async Task<(bool Success, string ErrorMsg, User? User)> CheckUserIsAdminAsync(HttpContext httpContext)
    {
        var info = await TryGetSelfUserAsync(httpContext, true);
        if (!info.Success)
        {
            return (false, info.ErrorMsg, null);
        }
        var user = info.User!;
        if (user.Role.IsAdmin)
        {
            return (true, "", user);
        }
        return (false, "Вы не администратор.", null);
    }
    
    public (bool Succes, Guid? UserId, string ErrorMsg) TryGetUserId(HttpContext httpContext)
    {
        var idClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == AuthOptions.ClaimTypeUserId);
        if (idClaim == null)
        {
            return (false, null, "У пользователя некорректные claims.");
        }
        
        return (true, new Guid(idClaim.Value), "");
    }
}