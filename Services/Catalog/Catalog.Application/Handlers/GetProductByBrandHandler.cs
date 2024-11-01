using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers
{
    public class GetProductByBrandHandler : IRequestHandler<GetProductByBrandQuery, IList<ProductResponse>>
    {
        public IProductRepository _productRepository { get; }
        public GetProductByBrandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public async Task<IList<ProductResponse>> Handle(GetProductByBrandQuery request, CancellationToken cancellationToken)
        {
            var productList =  await _productRepository.GetProductsByBrand(request.BrandName);
            var result = ProductMapper.mapper.Map<IList<ProductResponse>>(productList);
            return result;
        }
    }
}
