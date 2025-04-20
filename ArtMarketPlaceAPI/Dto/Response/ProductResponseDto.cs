namespace ArtMarketPlaceAPI.Dto.Response
{
    public class ProductResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Stock { get; set; }
        public string Image { get; set; } = string.Empty;
        public bool Available { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public UserResponseDto Artisan { get; set; } = null!;
        public CategoryResponseDto Category { get; set; } = null!;
        //Review
        //Customization

    }
}
