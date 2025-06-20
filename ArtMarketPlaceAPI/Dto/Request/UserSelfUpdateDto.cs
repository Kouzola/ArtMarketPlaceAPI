﻿namespace ArtMarketPlaceAPI.Dto.Request
{
    public class UserSelfUpdateDto
    {
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty ;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Role {  get; set; } = string.Empty;

    }
}
