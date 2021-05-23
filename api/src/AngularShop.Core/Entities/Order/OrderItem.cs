namespace AngularShop.Core.Entities.Order
{
    public class OrderItem : BaseEntity
    {
        public OrderProduct OrderProduct { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        protected OrderItem() { }

        public OrderItem(OrderProduct orderProduct, decimal price, int quantity)
        {
            OrderProduct = orderProduct;
            Price = price;
            Quantity = quantity;
        }
    }
}