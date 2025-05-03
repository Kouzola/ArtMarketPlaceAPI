using Business_Layer.Exceptions;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces.Product;
using Domain_Layer.Interfaces.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class ReviewService(IReviewRepository repository, IProductService productService) : IReviewService
    {
        private readonly IReviewRepository _repository = repository;
        private readonly IProductService _productService = productService;
        public async Task<Review> AddReviewAsync(Review review)
        {
            var product = await _productService.GetProductByIdAsync(review.ProductId);//Throw a NotFoundException si il trouve pas le produit
            return await _repository.AddReviewAsync(review);
        }

        public async Task<bool> DeleteReviewAsync(int id)
        {
            return await _repository.DeleteReviewAsync(id);
        }

        public async Task<IEnumerable<Review>> GetAllReviewsOfAProductAsync(int productId)
        {
            return await _repository.GetAllReviewsOfAProductAsync(productId);
        }

        public async Task<Review> GetReviewBydIdAsync(int id)
        {
            var review = await _repository.GetReviewBydIdAsync(id);
            if (review == null) throw new NotFoundException("Review not found!");
            return review;
        }

        public async Task<Review> RespondToAReview(int id, string response)
        {
            var review = await _repository.GetReviewBydIdAsync(id);
            if (review == null) throw new NotFoundException("Review not found!");
            review.ArtisanAnswer = response;
            return await UpdateReviewAsync(review);
        }

        public async Task<Review> UpdateReviewAsync(Review review)
        {
            var product = await _productService.GetProductByIdAsync(review.ProductId);
            var updatedReview = await _repository.UpdateReviewAsync(review);
            if (updatedReview == null) throw new NotFoundException("Review not found!");
            return updatedReview;
        }
    }
}
