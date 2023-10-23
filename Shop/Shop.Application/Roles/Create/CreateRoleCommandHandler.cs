using Common.Application;
using Shop.Domain.RoleAgg;
using Shop.Domain.RoleAgg.Repository;

namespace Shop.Application.Roles.Create;

public class CreateRoleCommandHandler : IBaseCommandHandler<CreateRoleCommand,long>
{
    private readonly IRoleRepository _repository;

    public CreateRoleCommandHandler(IRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult<long>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var permissions = new List<RolePermission>();
        request.Permissions.ForEach(f =>
        {
            permissions.Add(new RolePermission(f));
        });
        var role = new Role(request.Title, permissions);
         _repository.Add(role);
        await _repository.Save();

        return OperationResult<long>.Success(role.Id);
    }
}