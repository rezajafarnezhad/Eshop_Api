using Common.Application;
using Shop.Domain.SiteEntities.Repositories;

namespace Shop.Application.SiteEntities.Sliders.Delete;

public class DeleteSliderCommandHandler : IBaseCommandHandler<DeleteSliderCommand>
{
    private readonly ISliderRepository _sliderRepository;

    public DeleteSliderCommandHandler(ISliderRepository sliderRepository)
    {
        _sliderRepository = sliderRepository;
    }

    public async Task<OperationResult> Handle(DeleteSliderCommand request, CancellationToken cancellationToken)
    {
        _sliderRepository.Delete(request.SliderId);
        await _sliderRepository.Save();
        return OperationResult.Success();
    }
}