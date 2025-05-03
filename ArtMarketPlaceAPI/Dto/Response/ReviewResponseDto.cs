namespace ArtMarketPlaceAPI.Dto.Response
{
    public class ReviewResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Score { get; set; }
        public string? ArtisanAnswer { get; set; } 
        public int ProductId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
