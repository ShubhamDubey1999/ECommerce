using Basket.Application.Commands;
using Basket.Application.GrpcService;
using Basket.Application.Queries;
using Basket.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    public class BasketController : ApiController
    {
        private readonly IMediator _mediator;
        private DiscountGrpcService _discountGrpcService;

        public BasketController(Mediator mediator,DiscountGrpcService discountGrpcService)
        {
            this._mediator = mediator;
            _discountGrpcService = discountGrpcService;
        }
        [HttpGet]
        [Route("[action]/{username}", Name = "GetBasketByUsername")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartResponse>> GetBasket(string username)
        {
            var query = new GetBasketByUsernameQuery(username);
            var basket = await _mediator.Send(query);
            return Ok(basket);
        }
        [HttpPost("CreateBasket")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartResponse>> CreateBasket([FromBody] CreateShoppingCartCommand createShoppingCartCommand)
        {
            foreach (var item in createShoppingCartCommand.Items)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }
            var basket = await _mediator.Send(createShoppingCartCommand);
            return Ok(basket);
        }
        [HttpDelete]
        [Route("[action]/{username}", Name = "DeleteBasketByUsername")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteBasket(string username)
        {
            var query = new DeleteBasketByUsernameCommand(username);
            return Ok(await _mediator.Send(query));
        }
    }
}
