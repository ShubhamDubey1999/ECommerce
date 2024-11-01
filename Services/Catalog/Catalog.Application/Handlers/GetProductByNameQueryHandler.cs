using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers
{
    public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, IList<ProductResponse>>
    {
        public IProductRepository _productRepository { get; }
        public GetProductByNameQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public async Task<IList<ProductResponse>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductsByName(request.Name);
            var result = ProductMapper.mapper.Map<IList<ProductResponse>>(product);
            return result;
        }
    }
}
