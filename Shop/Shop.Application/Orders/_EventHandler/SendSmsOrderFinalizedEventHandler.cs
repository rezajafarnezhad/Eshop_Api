using MediatR;
using Shop.Domain.OrderAgg.Events;

namespace Shop.Application.Orders._EventHandler;

public class SendSmsOrderFinalizedEventHandler : INotificationHandler<OrderFinalized>
{
     
    public Task Handle(OrderFinalized notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}