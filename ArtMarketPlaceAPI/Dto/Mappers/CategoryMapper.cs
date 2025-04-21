using ArtMarketPlaceAPI.Dto.Response;
using Domain_Layer.Entities;

namespace ArtMarketPlaceAPI.Dto.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryResponseDto MapToDto(this Category category)
        {
            return new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Products = category.Products.Select(p => p.Name).ToList(),
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt,
            };
        }
    }
}
