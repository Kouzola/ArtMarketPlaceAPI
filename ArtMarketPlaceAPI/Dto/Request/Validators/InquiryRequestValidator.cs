using FluentValidation;

namespace ArtMarketPlaceAPI.Dto.Request.Validators
{
    public class InquiryRequestValidator : AbstractValidator<InquiryRequestDto>
    {
        public InquiryRequestValidator() 
        {
            RuleFor(i => i.Title).NotEmpty().WithMessage("You must enter a title!");
            RuleFor(i => i.Description).NotEmpty().WithMessage("You must enter a description!");
        }
    }
}
