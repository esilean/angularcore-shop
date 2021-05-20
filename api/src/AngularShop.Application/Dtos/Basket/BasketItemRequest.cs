using FluentValidation;

namespace AngularShop.Application.Dtos.Basket
{
    public class BasketItemRequest
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
    }

    public class BasketItemRequestValidator : AbstractValidator<BasketItemRequest>
    {
        public BasketItemRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.ProductName).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty().GreaterThan(0);
            RuleFor(x => x.PictureUrl).NotEmpty();
            RuleFor(x => x.Brand).NotEmpty();
            RuleFor(x => x.Type).NotEmpty();
        }
    }
}