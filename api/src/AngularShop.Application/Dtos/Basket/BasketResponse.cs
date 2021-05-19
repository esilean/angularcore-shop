using System.Collections.Generic;

namespace AngularShop.Application.Dtos.Basket
{
    public class BasketResponse
    {
        public string Id { get; set; }
        public List<BasketItemResponse> Items { get; set; }
        public BasketResponse(string id)
        {
            Id = id;
            Items = new List<BasketItemResponse>();
        }
    }
}