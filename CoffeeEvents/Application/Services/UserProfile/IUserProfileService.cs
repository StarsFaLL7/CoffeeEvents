using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Services.UserProfile;

public interface IUserProfileService
{
    public Task UpdateUserAvatarAsync(Guid userId, string webRootPath, string imagesFolderRelativePath,
        string defaultAvatarFilename, IFormFile? newFile);

    public Task<UserEvent[]> GetCreatedEventsAsync(Guid userId, int skip, int take, string? search = null);
}