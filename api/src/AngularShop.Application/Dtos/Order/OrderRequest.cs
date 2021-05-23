using FluentValidation;

namespace AngularShop.Application.Dtos.Order
{
    public class OrderRequest
    {
        public string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public OrderAddressRequest ShipToAddress { get; set; }
    }

    public class OrderRequestValidator : AbstractValidator<OrderRequest>
    {
        public OrderRequestValidator()
        {
            RuleFor(x => x.BasketId).NotEmpty();
            RuleFor(x => x.DeliveryMethodId).NotEmpty();
            RuleFor(x => x.ShipToAddress).NotEmpty();
        }
    }
}