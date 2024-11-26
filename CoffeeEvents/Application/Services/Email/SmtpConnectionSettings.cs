using System.Net;

namespace Application.Services.Email;

public class SmtpConnectionSettings
{
    public required string SmtpServer { get; init; }
    
    public required NetworkCredential AuthCredentials { get; init; }
    
    public int Port { get; init; } = 587;
    
    public bool EnableSsl { get; init; } = true;
}