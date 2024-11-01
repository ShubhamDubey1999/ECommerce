using Basket.Application.Responses;
using Basket.Core.Entities;
using Basket.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Commands
{
    public class CreateShoppingCartCommand : IRequest<ShoppingCartResponse>
    {
        public string Username { get; set; }
        public List<ShoppingCartItem> Items { get; set; }
        public CreateShoppingCartCommand(string Username, List<ShoppingCartItem> Items)
        {
            this.Username = Username;
            this.Items = Items;
        }
    }
}
