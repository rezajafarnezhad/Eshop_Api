namespace Common.Application.EmailUtil.EmailService;

public interface IEmailService
{
    Task Send(string userEmail, string body, string subject);
}