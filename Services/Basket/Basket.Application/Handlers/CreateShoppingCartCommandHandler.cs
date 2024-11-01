using Basket.Application.Commands;
using Basket.Application.Mappers;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using MediatR;
using Basket.Core.Entities;

namespace Basket.Application.Handlers
{
    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {
        readonly IBasketRepository _basketRepository;
        public CreateShoppingCartCommandHandler(IBasketRepository _basketRepository) { this._basketRepository = _basketRepository; }

        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            // TODO: will be integrating discount service here
            var shoppingcart = await _basketRepository.UpdateBasket(new ShoppingCart
            {
                Items = request.Items,
                UserName = request.Username
            });
            var shoppingCartResponse = BasketMapper.Mapper.Map<ShoppingCartResponse>(shoppingcart);
            return shoppingCartResponse;
        }
    }
}
