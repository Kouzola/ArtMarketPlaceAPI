namespace ArtMarketPlaceAPI.Dto.Response
{
    public class CartResponseDto
    {
        public int Id { get; set; }
        public List<CartItemResponseDto> Items { get; set; } = new List<CartItemResponseDto>();
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
    }

    public class CartItemResponseDto
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }
    }
}
