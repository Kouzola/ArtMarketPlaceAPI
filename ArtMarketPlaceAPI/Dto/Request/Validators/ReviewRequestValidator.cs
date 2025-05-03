using FluentValidation;

namespace ArtMarketPlaceAPI.Dto.Request.Validators
{
    public class ReviewRequestValidator : AbstractValidator<ReviewRequestDto>
    {
        public ReviewRequestValidator()
        {
            RuleFor(r => r.Title).NotEmpty().WithMessage("A title is required!");
            RuleFor(r => r.Description).NotEmpty().WithMessage("A description is required!");
            RuleFor(r => r.Score).GreaterThan(0).LessThan(6).WithMessage("The score must be between 1 and 5");
        }
    }
}
