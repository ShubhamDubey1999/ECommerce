using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers
{
    public class GetBaskeyByUsernameHandler : IRequestHandler<GetBasketByUsernameQuery, ShoppingCartResponse>
    {
        readonly IBasketRepository _basketRepository;
        public GetBaskeyByUsernameHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task<ShoppingCartResponse> Handle(GetBasketByUsernameQuery request, CancellationToken cancellationToken)
        {
            var shoppingCart = await (_basketRepository.GetBasket(request.Username));
            var shoppingCartResponse = BasketMapper.Mapper.Map<ShoppingCartResponse>(shoppingCart);
            return shoppingCartResponse;
        }
    }
}
