using ArtMarketPlaceAPI.Dto.Request;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtMarketPlaceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthentificationController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthentificationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginRequestDto loginRequest)
        {
            var token = await _userService.Login(loginRequest.UserName, loginRequest.Password);
            return Ok(new { token });
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterRequestDto registerRequest)
        {
            if (registerRequest.Role == Role.Admin) return BadRequest("Register failed : Role Invalid!");
            bool response = await _userService.Register(registerRequest.UserName, registerRequest.FirstName, registerRequest.LastName, 
                registerRequest.Email, registerRequest.Password, registerRequest.Role);
            if (response) return Ok("Register succesfully!");
            else return BadRequest("Register failed!");
        }
    }
}
