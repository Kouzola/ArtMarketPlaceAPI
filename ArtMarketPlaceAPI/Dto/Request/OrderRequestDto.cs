using Domain_Layer.Entities;

namespace ArtMarketPlaceAPI.Dto.Request
{
    public class OrderRequestDto
    {
        public int OrderId {  get; set; }
        public int ArtisanId { get; set; }
        public int DeliveryPartnerId { get; set; }
        public int CustomerId { get; set; }
        public int CartId { get; set; }
        public ShippingOption ShippingOption { get; set; }
        public PaymentDetailRequestDto? PaymentDetailRequestDto { get; set; }
    }
}
