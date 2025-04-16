using ArtMarketPlaceAPI.Dto.Mappers;
using ArtMarketPlaceAPI.Dto.Request;
using Azure.Core;
using Domain_Layer.Interfaces.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtMarketPlaceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    //TODO : Vérifier les endpoints de la consigne pour adapter peut-être
    public class UserController : ControllerBase
    {
        //CERTAIN GET PEUVENT ETRE AVEC D4AUTRE ROLE QUE ADMIN SINON TT LE RESTE C ADMIN
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        #region USER

        #region GET
        [HttpGet("admin/users")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("admin/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }

        [HttpGet("{username}")]
        [Authorize]
        public async Task<IActionResult> GetUserByUserName(string username)
        {
            if (username.Trim() == "admin") return NotFound("User not found!");
            var user = await _userService.GetUserByUsernameAsync(username);
            return Ok(user.MapToDto());
        }
        #endregion

        #region POST
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUser(UserRequestDtoForAdmin request)
        {
            var user = await _userService.AddUserAsync(new Domain_Layer.Entities.User
            {
                UserName = request.UserName,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Role = request.Role,
            });
            return Ok(user);
        }
        #endregion

        #region PUT
        //Update pour admin. L'admin peut tt update mais pour le password il n'est pas obliger.
        [HttpPut("admin/{id:int}")]
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
                });
            return Ok(user);
        }

        //Manage Profile pour User normal
        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateUserForUser(UserSelfUpdateDto request, int id)
        {
            //Check le token claim avec l'id
            var userIdFromToken = int.Parse(User.FindFirst("id")?.Value ?? "0");

            if (userIdFromToken != id)
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
                });

            return Ok(user.MapToSelfResponseDto());
        }
        #endregion

        #region DELETE
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserById(int id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok($"User with id : {id} deleted!");
        }
        #endregion

        #endregion

    }
}
