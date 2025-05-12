using FluentValidation;

namespace ArtMarketPlaceAPI.Dto.Request.Validators
{
    public class CartRequestValidator : AbstractValidator<CartRequestDto>
    {
        public CartRequestValidator()
        {
            RuleFor(c => c.Quantity).GreaterThan(0).WithMessage("Must have a quantity greather than 0!");
            RuleFor(c => c.UserId).GreaterThan(0).WithMessage("Invalid UserId");
            RuleFor(c => c.ProductId).GreaterThan(0).WithMessage("Invalid ProductId");
        }
    }
}
