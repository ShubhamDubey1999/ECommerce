using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using Catalog.Core.Specs;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, Pagination<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;
        public GetAllProductsHandler(IProductRepository _productRepository)
        {
            this._productRepository = _productRepository;
        }
        public async Task<Pagination<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetProducts(request.CatalogSpecParams);
            // I did this earlier
            //var result = ProductMapper.mapper.Map<IList<Product>, IList<ProductResponse>>(products.ToList());
            // but now this
            var result = ProductMapper.mapper.Map<Pagination<ProductResponse>>(products);
            return result;
        }
    }
}
