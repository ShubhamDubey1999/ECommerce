using AutoMapper;
using Discount.Application.Commands;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Handlers
{
    public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, CouponModel>
    {
        readonly IDiscountRepository _discountRepository;
        readonly IMapper mapper;

        public CreateDiscountCommandHandler(IDiscountRepository discountRepository,IMapper mapper)
        {
            this._discountRepository = discountRepository;
            mapper = mapper;
        }


        public async Task<CouponModel> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var coupon = mapper.Map<Coupon>(request);
            await _discountRepository.CreateDiscount(coupon);
            return mapper.Map<CouponModel>(coupon);
        }
    }
}
