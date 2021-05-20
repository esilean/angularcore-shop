using FluentValidation;

namespace AngularShop.Application.Validators
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var opts = ruleBuilder
                .NotEmpty()
                .MinimumLength(3).WithMessage("Must be at least 6 chacacters")
                .Matches("[a-z]").WithMessage("Must contain at least 1 lowercase")
                .Matches("[0-9]").WithMessage("Must contain at least 1 number");

            return opts;
        }
    }
}