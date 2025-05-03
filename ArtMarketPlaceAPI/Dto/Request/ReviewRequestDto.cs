namespace ArtMarketPlaceAPI.Dto.Request
{
    public class ReviewRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Score { get; set; }
    }
}
