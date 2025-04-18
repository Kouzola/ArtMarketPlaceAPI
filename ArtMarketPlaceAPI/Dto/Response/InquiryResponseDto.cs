namespace ArtMarketPlaceAPI.Dto.Response
{
    public class InquiryResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool WantConsultation { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public UserResponseDto Customer { get; set; } = null!;
        public UserResponseDto Artisan { get; set; } = null!;
    }
}
