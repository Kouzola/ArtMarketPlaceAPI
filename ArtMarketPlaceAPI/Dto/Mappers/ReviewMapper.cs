using ArtMarketPlaceAPI.Dto.Response;
using Domain_Layer.Entities;

namespace ArtMarketPlaceAPI.Dto.Mappers
{
    public static class ReviewMapper
    {
        public static ReviewResponseDto MapToDto(this Review review)
        {
            return new ReviewResponseDto
            {
                Id = review.Id,
                Title = review.Title,
                Description = review.Description,
                Score = review.Score,
                ArtisanAnswer = review.ArtisanAnswer,
                CreatedAt = review.CreatedAt,
                UpdatedAt = review.UpdatedAt,
                CustomerId = review.CustomerId,
            };
        }
    }
}
