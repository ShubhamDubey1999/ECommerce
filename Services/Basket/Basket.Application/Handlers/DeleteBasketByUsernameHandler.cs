using Basket.Application.Commands;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers
{
    public class DeleteBasketByUsernameHandler : IRequestHandler<DeleteBasketByUsernameCommand, Unit>
    {
        readonly IBasketRepository _repository;
        public DeleteBasketByUsernameHandler(IBasketRepository repository)
        {
            this._repository = repository;
        }
        public async Task<Unit> Handle(DeleteBasketByUsernameCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteBasket(request.Username);
            return Unit.Value;
        }
    }
}
