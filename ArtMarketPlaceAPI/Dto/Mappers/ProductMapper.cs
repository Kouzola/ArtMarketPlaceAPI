using ArtMarketPlaceAPI.Dto.Response;
using Domain_Layer.Entities;

namespace ArtMarketPlaceAPI.Dto.Mappers
{
    public static class ProductMapper
    {
        public static ProductResponseDto MapToDto(this Product product)
        {
            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Reference = product.Reference,
                Price = product.Price,
                Stock = product.Stock,
                Image = product.Image,
                Available = product.Available,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
                Artisan = product.Artisan.MapToDto(),
                Category = product.Category.MapToDto(),
            };
        }
    }
}
