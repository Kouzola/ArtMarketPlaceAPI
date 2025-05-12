namespace ArtMarketPlaceAPI.Dto.Request
{
    public class ProductRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Stock { get; set; }
        public IFormFile ImageFile { get; set; } = null!;
        public bool Available { get; set; }
        public int ArtisanId { get; set; }
        public int CategoryId { get; set; }
    }
}
