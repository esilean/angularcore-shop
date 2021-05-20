using System.Collections.Generic;
using FluentValidation;

namespace AngularShop.Application.Dtos.Basket
{
    public class BasketRequest
    {
        public string Id { get; set; }
        public List<BasketItemRequest> Items { get; set; }
    }

    public class BasketRequestValidator : AbstractValidator<BasketRequest>
    {
        public BasketRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleForEach(x => x.Items).SetValidator(new BasketItemRequestValidator());
        }
    }
}