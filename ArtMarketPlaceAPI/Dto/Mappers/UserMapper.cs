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
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                FullName = user.FullName,
                Email = user.Email,
            };
        }
    }
}
