using FluentValidation;

namespace ArtMarketPlaceAPI.Dto.Request.Validators
{
    public class PaymentDetailRequestValidator :AbstractValidator<PaymentDetailRequestDto>
    {
        public PaymentDetailRequestValidator() 
        {
            RuleFor(pd => pd.PaymentMethod).NotEmpty().WithMessage("You must choose a method payment!");
            RuleFor(pd => pd.Amout).GreaterThan(0).WithMessage("Invalid Amount!");
        }
    }
}
