namespace ArtMarketPlaceAPI.Dto.Response
{
    public class CategoryResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<ProductResponseDto> Products { get; set; } = new List<ProductResponseDto>();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
