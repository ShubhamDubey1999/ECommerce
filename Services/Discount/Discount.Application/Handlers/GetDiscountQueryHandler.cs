using Discount.Application.Queries;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Discount.Application.Handlers
{
    public class GetDiscountQueryHandler : IRequestHandler<GetDiscountQuery, CouponModel>
    {
        private readonly IDiscountRepository _discountRepository;
        //private readonly ILogger _logger;

        public GetDiscountQueryHandler(IDiscountRepository discountRepository)
        {
            this._discountRepository = discountRepository;
            //this._logger = logger;
        }
        public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
        {
            var coupon = await _discountRepository.GetDiscount(request.productName);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount for the Product Name = {request.productName} not found"));
            }
            var couponModel = new CouponModel
            {
                Id = coupon.Id,
                Amount = coupon.Amount,
                Description = coupon.Description,
                ProductName = coupon.ProductName
            };
            //_logger.LogInformation($"Coupon for the {request.productName} is fetched");
            return couponModel;
        }
    }
}
