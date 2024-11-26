using System.Net.Mail;
using Application.Services.Email.Interfaces;
using Domain.Entities;

namespace Application.Services.Email;

public class ApplicationEmailSender : IApplicationEmailSender
{
    private SmtpConnectionSettings ConnectionSettings { get; }
    
    public ApplicationEmailSender(SmtpConnectionSettings settings)
    {
        ConnectionSettings = settings;
    }
    
    public async Task SendEmailAsync(string subject, string authorEmail, string body, bool isHtml, params string[] toEmails)
    {
        var client = new SmtpClient(ConnectionSettings.SmtpServer)
        {
            Credentials = ConnectionSettings.AuthCredentials,
            Port = ConnectionSettings.Port,
            EnableSsl = ConnectionSettings.EnableSsl
        };
        var mailMessage = new MailMessage
        {
            From = new MailAddress(authorEmail),
            Subject = subject,
            Body = body,
            IsBodyHtml = isHtml
        };
        foreach (var mail in toEmails)
        {
            mailMessage.To.Add(mail);
        }
        
        await client.SendMailAsync(mailMessage);
    }

    public async Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
    {
        throw new NotImplementedException();
    }

    public async Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
    {
        throw new NotImplementedException();
    }

    public async Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
    {
        throw new NotImplementedException();
    }
}