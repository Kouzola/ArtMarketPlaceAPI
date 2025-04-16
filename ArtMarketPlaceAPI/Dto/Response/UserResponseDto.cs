using Domain_Layer.Entities;

namespace ArtMarketPlaceAPI.Dto.Response
{
    public class UserResponseDto
    {
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty ;
        public string LastName { get; set; } = string.Empty;
        public Role Role { get; set; }
        public string FullName {  get; set; } = string.Empty ;
    }

    //Dans le cas ou l'user manage son profile
    public class UserSelfResponseDto
    {
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email {  get; set; } = string.Empty;
        public Role Role { get; set; }
        public string FullName { get; set; } = string.Empty;
    }
}
