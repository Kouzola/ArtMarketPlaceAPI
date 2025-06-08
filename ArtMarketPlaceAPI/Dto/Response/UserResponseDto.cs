using Domain_Layer.Entities;

namespace ArtMarketPlaceAPI.Dto.Response
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty ;
        public string LastName { get; set; } = string.Empty;
        public Role Role { get; set; }
        public string FullName {  get; set; } = string.Empty ;
    }

    //Dans le cas ou l'user manage son profile
    public class UserSelfResponseDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email {  get; set; } = string.Empty;
        public bool Active { get; set; }
        public Role Role { get; set; }
        public string FullName { get; set; } = string.Empty;
        public Address Address { get; set; } = null!;
    }
}
