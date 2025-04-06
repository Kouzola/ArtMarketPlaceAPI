using Domain_Layer.Entities;
using FluentValidation;

namespace ArtMarketPlaceAPI.Dto.Request.Validators
{
    public class UserRequestForAdminValidator : AbstractValidator<UserRequestDtoForAdmin>
    {
        public UserRequestForAdminValidator() 
        {
            RuleFor(u => u.UserName).NotEmpty().WithMessage("An Username is required!")
                .MaximumLength(50).WithMessage("An Username cannot be more than 50 characters!");

            RuleFor(u => u.FirstName).NotEmpty().WithMessage("A FirstName is required!").
                MaximumLength(50).WithMessage("A FirstName cannot be more than 50 characters!");

            RuleFor(u => u.LastName).NotEmpty().WithMessage("A LastName is required!")
                .MaximumLength(50).WithMessage("A LastName cannot be more than 50 characters!");

            RuleFor(u => u.Email).NotEmpty().WithMessage("An Email is required!")
                .EmailAddress().WithMessage("An valid email address is required!");

            RuleFor(u => u.Role).Must(role => role != Role.Admin).WithMessage("You must choose a valid role!");
        }
    }
}
