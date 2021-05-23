namespace AngularShop.Core.Entities.Order
{
    public class OrderProduct
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }

        protected OrderProduct() { }

        public OrderProduct(int productId, string name, string pictureUrl)
        {
            ProductId = productId;
            Name = name;
            PictureUrl = pictureUrl;
        }
    }
}