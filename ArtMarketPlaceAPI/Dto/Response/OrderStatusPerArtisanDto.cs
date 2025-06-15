using Domain_Layer.Entities;

namespace ArtMarketPlaceAPI.Dto.Response
{
    public class OrderStatusPerArtisanDto
    {
        public int OrderId { get; set; }
        public int ArtisanId { get; set; }
        public int Status { get; set; } //0 -> PENDING , 1 -> CONFIRM, 2 -> SHIPPED
    }
}
