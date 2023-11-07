using System.Net;
using System.Net.Mail;
using System.Text;

namespace Common.Application.EmailUtil.EmailService;

public class EmailService : IEmailService
{
    public async Task Send(string userEmail, string body, string subject)
    {
        SmtpClient client = new SmtpClient();

        client.Port = 587;
        client.Host = "smtp.gmail.com";
        client.EnableSsl = true;
        client.Timeout = 1000000;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential("p", "p");
        MailMessage message = new MailMessage("p", userEmail, subject, body);
        message.From = new MailAddress("p@p.p", "ESHOP Ham");
        message.IsBodyHtml = true;
        message.BodyEncoding = Encoding.UTF8;
        message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
        client.Send(message);
        await Task.CompletedTask;
    }
}