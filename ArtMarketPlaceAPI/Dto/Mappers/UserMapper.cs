using ArtMarketPlaceAPI.Dto.Response;
using Domain_Layer.Entities;

namespace ArtMarketPlaceAPI.Dto.Mappers
{
    public static class UserMapper
    {
        public static UserResponseDto MapToDto(this User user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                FullName = user.FullName,
            };
        }

        public static UserSelfResponseDto MapToSelfResponseDto(this User user)
        {
            return new UserSelfResponseDto
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                FullName = user.FullName,
                Email = user.Email,
                Address = user.Address,
                Active = user.Active,
            };
        }
    }
}
