using ArtMarketPlaceAPI.Dto.Response;
using Domain_Layer.Entities;

namespace ArtMarketPlaceAPI.Dto.Mappers
{
    public static class InquiryMapper
    {
        public static InquiryResponseDto MapToDto(this Inquiry inquiry)
        {
            return new InquiryResponseDto
            {
                Id = inquiry.Id,
                Title = inquiry.Title,
                Description = inquiry.Description,
                WantConsultation = inquiry.WantConsultation,
                CreatedAt = inquiry.CreatedAt,
                UpdatedAt = inquiry.UpdatedAt,
                ArtisanResponse = (inquiry.ArtisanResponse ?? string.Empty),
                Customer = inquiry.Customer.MapToDto(),
                Artisan = inquiry.Artisan.MapToDto(),
            };
        }
    }
}
