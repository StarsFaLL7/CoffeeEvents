using Application.Services.UserProfile;
using Domain.DataQuery;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class UserProfileService : IUserProfileService
{
    private readonly BaseService<UserEvent> _eventsService;
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public UserProfileService(BaseService<UserEvent> eventsService, IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _eventsService = eventsService;
        _dbContextFactory = dbContextFactory;
    }
    
    public async Task UpdateUserAvatarAsync(Guid userId, string webRootPath, string imagesFolderRelativePath,
        string defaultAvatarFilename, IFormFile? newFile)
    {
        var dbContext = await _dbContextFactory.CreateDbContextAsync();
        var user = dbContext.Users.First(u => u.Id == userId);
        
        var imagePath = Path.Combine(webRootPath, user.AvatarImageFilepath);
        if (!string.IsNullOrWhiteSpace(user.AvatarImageFilepath) && !user.AvatarImageFilepath.Contains(defaultAvatarFilename) && File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }

        if (newFile == null)
        {
            user.AvatarImageFilepath = Path.Combine(imagesFolderRelativePath, defaultAvatarFilename);
        }
        else
        {
            var fileExtension = newFile.FileName.Split('.')[^1];
            var fileName = $"{user.Id}.{fileExtension}";
            var fullPath = Path.Combine(webRootPath, imagesFolderRelativePath, fileName);
            await using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await newFile.CopyToAsync(stream);
            }
            user.AvatarImageFilepath = Path.Combine(imagesFolderRelativePath, fileName);
        }
        
        await dbContext.SaveChangesAsync();
    }

    public async Task<UserEvent[]> GetCreatedEventsAsync(Guid userId, int skip, int take, string? search = null)
    {
        return await _eventsService.GetAsync(new DataQueryParams<UserEvent>
        {
            Expression = e => e.CreatorUserId == userId,
            Paging = new PagingParams
            {
                Skip = skip,
                Take = take
            },
            Sorting = new SortingParams<UserEvent>
            {
                OrderBy = e => e.DateStart,
                PropertyName = "DateStart",
                Ascending = true
            }
        });
    }
}