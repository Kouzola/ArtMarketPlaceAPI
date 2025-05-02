namespace ArtMarketPlaceAPI.Dto.Response
{
    public class PaymentDetailResponseDto
    {
        public string PaymentMethod { get; set; } = string.Empty;
        public double Amout { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
