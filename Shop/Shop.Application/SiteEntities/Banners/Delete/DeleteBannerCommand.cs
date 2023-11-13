using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application;
using Shop.Domain.SiteEntities.Repositories;

namespace Shop.Application.SiteEntities.Banners.Delete;

public record DeleteBannerCommand(long BannerId) : IBaseCommand;

public class DeleteBannerCommandHandler : IBaseCommandHandler<DeleteBannerCommand>
{
    private readonly IBannerRepository _bannerRepository;

    public DeleteBannerCommandHandler(IBannerRepository blogRepository)
    {
        _bannerRepository = blogRepository;
    }

    public async Task<OperationResult> Handle(DeleteBannerCommand request, CancellationToken cancellationToken)
    {
        _bannerRepository.Delete(request.BannerId);
        await _bannerRepository.Save();
        return OperationResult.Success();
    }
}