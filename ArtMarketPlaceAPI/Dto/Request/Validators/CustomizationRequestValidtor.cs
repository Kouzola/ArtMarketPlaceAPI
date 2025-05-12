using FluentValidation;

namespace ArtMarketPlaceAPI.Dto.Request.Validators
{
    public class CustomizationRequestValidtor : AbstractValidator<CustomizationRequestDto>
    {
        public CustomizationRequestValidtor()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("A name is required!");
            RuleFor(c => c.Description).NotEmpty().WithMessage("A description is required!");
            RuleFor(c => c.Price).GreaterThan(0).WithMessage("The price must be greather than 0!");
            RuleFor(p => p.ProductId).GreaterThan(0).WithMessage("An product must be specified!");
        }
    }
}
