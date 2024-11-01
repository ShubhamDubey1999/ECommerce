using Basket.Application.Commands;
using Basket.Application.Mappers;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using MediatR;
using Basket.Core.Entities;
using Basket.Application.GrpcService;

namespace Basket.Application.Handlers
{
    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {
        readonly IBasketRepository _basketRepository;
        DiscountGrpcService _discountGrpcService;
        public CreateShoppingCartCommandHandler(IBasketRepository _basketRepository, DiscountGrpcService discountGrpcService)
        { 
            this._basketRepository = _basketRepository;
            _discountGrpcService = discountGrpcService;
        }

        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            foreach (var item in request.Items)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }
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
