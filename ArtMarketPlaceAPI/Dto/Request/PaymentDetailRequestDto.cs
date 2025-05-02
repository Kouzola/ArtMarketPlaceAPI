namespace ArtMarketPlaceAPI.Dto.Request
{
    public class PaymentDetailRequestDto
    {
        public string PaymentMethod { get; set; } = string.Empty;
        public double Amout { get; set; }

    }
}
