using ArtMarketPlaceAPI.Dto.Mappers;
using ArtMarketPlaceAPI.Dto.Request;
using Domain_Layer.Interfaces.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtMarketPlaceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReviewController(IReviewService reviewService) : ControllerBase
    {
        private readonly IReviewService _reviewService = reviewService;
        #region GET
        [HttpGet("{productId:int}")]
        public async Task<IActionResult> GetAllReviewsOfAProduct(int productId)
        {
            var reviews = await _reviewService.GetAllReviewsOfAProductAsync(productId);
            return Ok(reviews.Select(r => r.MapToDto()));
        }

        [HttpGet("by-Id/{id:int}")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            var review = await _reviewService.GetReviewBydIdAsync(id);
            return Ok(review.MapToDto());
        }
        #endregion

        #region POST
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddAReviewToAProduct(int productId,ReviewRequestDto request)
        {
            var customerId = User.FindFirst("id")?.Value;

            var review = await _reviewService.AddReviewAsync(new Domain_Layer.Entities.Review
            {
                ProductId = productId,
                Title = request.Title,
                Description = request.Description,
                Score = request.Score,
                CustomerId = int.Parse(customerId!)
            });
            return Ok(review.MapToDto());
        }
        #endregion

        #region PUT
        [HttpPut("{reviewId:int}")]
        [Authorize (Roles = "Artisan")]
        public async Task<IActionResult> AnswerToAReviewByArtisant(int reviewId, [FromBody] string answer)
        {
            var review = await _reviewService.RespondToAReview(reviewId, answer);
            return Ok(review.MapToDto());
        }
        #endregion

        #region DELETE
        [HttpDelete("{id:int}")]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var isDeleted = await _reviewService.DeleteReviewAsync(id);
            if (!isDeleted) return NotFound("Review not found!");
            return Ok("Review deleted!");
        }
        #endregion

    }
}
