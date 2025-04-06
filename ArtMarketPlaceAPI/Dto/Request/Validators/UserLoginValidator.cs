using ArtMarketPlaceAPI.Dto.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Validators
{
    public class UserLoginValidator : AbstractValidator<UserLoginRequestDto>
    {
        public UserLoginValidator() 
        {
            RuleFor(u => u.UserName).NotEmpty().WithMessage("An Username is required !");
            RuleFor(u => u.Password).NotEmpty().WithMessage("A password is required !");
        }
    }
}
