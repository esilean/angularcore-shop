using System.Collections.Generic;

namespace AngularShop.Application.Dtos.Basket
{
    public class BasketToReturn
    {
        public string Id { get; set; }
        public List<BasketItemToReturn> Items { get; set; }
        public BasketToReturn(string id)
        {
            Id = id;
            Items = new List<BasketItemToReturn>();
        }
    }
}