using Domain.Entities;

namespace Application.Services.Email.Interfaces;

public interface IIdentityEmailSender
{
    public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink);

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink);

    public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode);
}