using ArtMarketPlaceAPI.Dto.Response;

namespace ArtMarketPlaceAPI.Dto.Request
{
    public class InquiryRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool WantConsultation { get; set; }
        public int CustomerId { get; set; }
        public int ArtisanId { get; set; }
    }
}
