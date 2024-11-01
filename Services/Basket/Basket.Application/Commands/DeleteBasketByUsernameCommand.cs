using Basket.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Commands
{
    public class DeleteBasketByUsernameCommand : IRequest<Unit>
    {
        public string Username { get; set; }
        public DeleteBasketByUsernameCommand(string username)
        {
            Username = username;
        }
    }
}
