namespace AngularShop.Application.Dtos.Order
{
    public class OrderItemResponse
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}