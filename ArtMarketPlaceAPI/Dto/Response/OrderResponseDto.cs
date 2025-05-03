using Domain_Layer.Entities;

namespace ArtMarketPlaceAPI.Dto.Response
{
    public class OrderResponseDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public ShippingOption ShippingOption { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public PaymentDetailResponseDto? PaymentDetail { get; set; }
        public List<ShipmentResponseDto> Shipments { get; set; } = new List<ShipmentResponseDto>();
        public UserResponseDto Customer { get; set; } = null!;
    }

    public class OrderResponseForArtisanDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public ShippingOption ShippingOption { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public UserResponseDto Customer { get; set; } = null!;
        public List<ProductOrderInfoDto> ProductsOrderedInfo { get; set; } = new List<ProductOrderInfoDto>();

    }
}
