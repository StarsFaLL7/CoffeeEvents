namespace Application.Services.Email.Interfaces;

public interface IApplicationEmailSender : IIdentityEmailSender
{
    public Task SendEmailAsync(string subject, string authorEmail, string body, bool isHtml, params string[] toEmails);
}