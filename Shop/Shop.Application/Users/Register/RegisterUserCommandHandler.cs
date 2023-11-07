using Common.Application;
using Common.Application.SecurityUtil;
using MediatR;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Events;
using Shop.Domain.UserAgg.Repository;
using Shop.Domain.UserAgg.Services;

namespace Shop.Application.Users.Register;

internal class RegisterUserCommandHandler : IBaseCommandHandler<RegisterUserCommand>
{
    private readonly IUserRepository _repository;
    private readonly IUserDomainService _domainService;
    private readonly IMediator _mediator;
    public RegisterUserCommandHandler(IUserRepository repository, IUserDomainService domainService, IMediator mediator)
    {
        _repository = repository;
        _domainService = domainService;
        _mediator = mediator;
    }

    public async Task<OperationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.RegisterUser(request.PhoneNumber.Value, Sha256Hasher.Hash(request.Password), _domainService);
        
        _repository.Add(user);
        await _repository.Save();
        await _mediator.Publish(new UserRegistered(user.Id,user.PhoneNumber));
        return OperationResult.Success("کاربر گرامی به همتا شاپ خوش آمدید!");
    }
}