using Domain_Layer.Entities;

namespace ArtMarketPlaceAPI.Dto.Response
{
    public class ShipmentResponseDto
    {
        public int Id { get; set; }
        public DateTime? ShippingDate { get; set; }
        public DateTime? EstimatedArrivalDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public string TrackingNumber { get; set; } = string.Empty;
        public ShipmentStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string OrderCode { get; set; } = string.Empty;
        public UserResponseDto DeliveryPartner { get; set; } = null!;
        public List<int> Products { get; set; } = new List<int>();
    }
}
