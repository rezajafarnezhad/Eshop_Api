using Common.Query;
using Shop.Domain.ProductAgg.Repository;

namespace Shop.Query.Products.GetForDropDown;
public class GetForDropDownQuery : IQuery<Dictionary<long, string>> { }

public class GetForDropDownQueryHandler : IQueryHandler<GetForDropDownQuery, Dictionary<long, string>>
{

    private readonly IProductRepository _productRepository;

    public GetForDropDownQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Dictionary<long, string>> Handle(GetForDropDownQuery request, CancellationToken cancellationToken)
    {
        return await _productRepository.GetProductsForDropDown();
    }
}