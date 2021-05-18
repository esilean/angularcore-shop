using System.Collections.Generic;

namespace AngularShop.Application.Dtos.Basket
{
    public class BasketRequest
    {
        public string Id { get; set; }
        public List<BasketItemRequest> Items { get; set; }
    }
}