﻿using Catalog.Application.Commands;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers
{
    public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductByIdCommandHandler(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }
        public Task<bool> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
        {
            var result = _productRepository.DeleteProductId(request.Id); return result;
        }
    }
}
