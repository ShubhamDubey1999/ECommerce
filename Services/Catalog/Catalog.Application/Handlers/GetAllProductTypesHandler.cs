using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetAllProductTypesHandler : IRequestHandler<GetAllProductsTypesQuery, IList<TypesResponse>>
    {
        private readonly ITypesRepository _typesRepository;

        public GetAllProductTypesHandler(ITypesRepository typesRepository)
        {
            this._typesRepository = typesRepository;
        }
        public async Task<IList<TypesResponse>> Handle(GetAllProductsTypesQuery request, CancellationToken cancellationToken)
        {
            var productTypes = await _typesRepository.GetAllTypes();
            var result = ProductMapper.mapper.Map<IList<TypesResponse>>(productTypes);
            return result;
        }
    }
}
