using System.Collections.Generic;

namespace AngularShop.Core.Entities
{
    public class Basket
    {
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();

        protected Basket() { }

        public Basket(string id)
        {
            Id = id;
        }
    }
}