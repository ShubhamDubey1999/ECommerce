﻿using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Queries
{
    public class GetDiscountQuery : IRequest<CouponModel>
    {
        public string productName { get; set; }

        public GetDiscountQuery(string ProductName)
        {
            productName = ProductName;
        }
    }
}
