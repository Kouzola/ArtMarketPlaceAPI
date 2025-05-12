using FluentValidation;

namespace ArtMarketPlaceAPI.Dto.Request.Validators
{
    public class CategoryRequestValidator : AbstractValidator<CategoryRequestDto>
    {
        public CategoryRequestValidator() 
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("A category name is required!");
            RuleFor(c => c.Description).NotEmpty().WithMessage("A description name is required!");
        }
    }
}
