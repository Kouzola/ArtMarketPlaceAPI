using ArtMarketPlaceAPI.Dto.Mappers;
using ArtMarketPlaceAPI.Dto.Request;
using Azure.Core;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ArtMarketPlaceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        #region GET
        [HttpGet("admin/users")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users.Select(u => u.MapToSelfResponseDto()));
        }

        [HttpGet("deliveryPartner")]
        [Authorize]
        public async Task<IActionResult> GetDeliveryPartner()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users.Select(u => u.MapToDto()).Where(u => u.Role == Role.Delivery));
        }

        [HttpGet("admin/users/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserByIdForAdmin(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user.MapToSelfResponseDto()); ;
        }

        [HttpGet("{username}")]
        [Authorize]
        public async Task<IActionResult> GetUserByUserName(string username)
        {
            if (username.Trim() == "admin") return NotFound("User not found!");
            var user = await _userService.GetUserByUsernameAsync(username);
            return Ok(user.MapToDto());
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Customer,Artisan,Delivery")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var currentUserId = User.FindFirst("id")?.Value;

            if (currentUserId != id.ToString()) return Forbid();

            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user.MapToSelfResponseDto());
        }
            #endregion

        #region PUT
            //Update pour admin. L'admin peut tt update mais pour le password il n'est pas obliger.
        [HttpPut("admin/users/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserForAdmin(UserRequestDtoForAdmin request, int id)
        {
            var user = await _userService.UpdateUserAsync(
                new Domain_Layer.Entities.User 
                {
                    Id = id,
                    UserName = request.UserName,
                    Password = request.Password,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Role = request.Role,
                    Active = request.Active,
                    Address = new Domain_Layer.Entities.Address { Street = request.Street, City = request.City, Country = request.Country, PostalCode = request.PostalCode }
                });
            return Ok(user);
        }

        //Manage Profile pour User normal
        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateUserForUser(UserSelfUpdateDto request, int id)
        {
            //Check le token claim avec l'id
            var currentUserId = User.FindFirst("id")?.Value;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value?.Trim();
            Enum.TryParse<Role>(userRole, true, out var parsedRole);

            if (currentUserId != id.ToString())
            {
                return BadRequest("Invalid Request!");
            }

            var user = await _userService.UpdateUserAsync(
                new Domain_Layer.Entities.User
                {
                    Id = id,
                    UserName = request.UserName,
                    Password = request.Password,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Role = parsedRole,
                    Address = new Domain_Layer.Entities.Address { Street = request.Street, City = request.City, Country = request.Country, PostalCode = request.PostalCode }
                });

            return Ok(user.MapToSelfResponseDto());
        }
        #endregion

        #region DELETE
        [HttpDelete("admin/users/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserById(int id)
        {
            var response = await _userService.DeleteUserAsync(id);
            if (!response) return BadRequest();
            return Ok();
        }
        #endregion


    }
}
