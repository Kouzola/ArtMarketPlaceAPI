using FluentValidation;

namespace ArtMarketPlaceAPI.Dto.Request.Validators
{
    public class ProductRequestValidator : AbstractValidator<ProductRequestDto>
    {
        public ProductRequestValidator() 
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("A name is required!");
            RuleFor(p => p.Description).NotEmpty().WithMessage("A description is required!");
            RuleFor(p => Path.GetExtension(p.ImageFile.FileName)).Must(extension => extension == ".jpg" || extension == ".png");
            RuleFor(p => p.Price).GreaterThan(0).WithMessage("The price must be greater than 0!");
            RuleFor(p => p.Stock).GreaterThanOrEqualTo(0).WithMessage("Le stock must be equal or greater than 0!");
            RuleFor(p => p.ArtisanId).GreaterThan(0).WithMessage("An artisan must be specified!");
            RuleFor(p => p.CategoryId).GreaterThan(0).WithMessage("An category must be specified!");
        }
    }
}
