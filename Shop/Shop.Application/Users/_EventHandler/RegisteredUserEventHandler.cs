using Common.Application.EmailUtil;
using Common.Application.EmailUtil.EmailService;
using MediatR;
using Shop.Domain.UserAgg.Events;

namespace Shop.Application.Users._EventHandler;

public class RegisteredUserEventHandler : INotificationHandler<UserRegistered>
{
    private readonly IEmailService _emailService;
    public RegisteredUserEventHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Handle(UserRegistered notification, CancellationToken cancellationToken)
    {
        string body = "";
        await _emailService.Send("p", body, "خوش آمدید");
        await Task.CompletedTask;
    }
}