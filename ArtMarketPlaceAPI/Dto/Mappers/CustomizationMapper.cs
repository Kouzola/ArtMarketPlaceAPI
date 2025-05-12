using ArtMarketPlaceAPI.Dto.Response;
using Domain_Layer.Entities;

namespace ArtMarketPlaceAPI.Dto.Mappers
{
    public static class CustomizationMapper
    {
        public static CustomizationResponseDto MapToDto(this Customization customization)
        {
            return new CustomizationResponseDto
            {
                Id = customization.Id,
                Name = customization.Name,
                Description = customization.Description,
                Price = customization.Price,
                CreatedAt = customization.CreatedAt,
                UpdatedAt = customization.UpdatedAt,
            };
        }
    }
}
