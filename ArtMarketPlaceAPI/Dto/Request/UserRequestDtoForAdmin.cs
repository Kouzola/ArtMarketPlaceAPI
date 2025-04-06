using Domain_Layer.Entities;

namespace ArtMarketPlaceAPI.Dto.Request
{
    public class UserRequestDtoForAdmin
    {
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public Role Role { get; set; }
    }
}
