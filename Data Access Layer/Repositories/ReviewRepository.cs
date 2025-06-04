using Data_Access_Layer.AppDbContext;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces.Review;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class ReviewRepository(ArtMarketPlaceDbContext context) : IReviewRepository
    {
        private readonly ArtMarketPlaceDbContext _context = context;

        public async Task<Review> AddReviewAsync(Review review)
        {
            var addedReview = await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return addedReview.Entity;
        }

        public async Task<bool> DeleteReviewAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return false;
            return true;
        }

        public async Task<IEnumerable<Review>> GetAllReviewsOfAProductAsync(int productId)
        {
            var reviews = await _context.Reviews.Where(r => r.ProductId == productId).ToListAsync();
            return reviews;
        }

        public async Task<Review?> GetReviewBydIdAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if(review == null) return null;
            return review;
        }

        public async Task<Review?> UpdateReviewAsync(Review review)
        {
            var reviewToUpdate = await _context.Reviews.FindAsync(review.Id);
            if (reviewToUpdate == null) return null;

            reviewToUpdate.Title = review.Title;
            reviewToUpdate.Description = review.Description;
            reviewToUpdate.Score = review.Score;
            reviewToUpdate.ArtisanAnswer = review.ArtisanAnswer;

            await _context.SaveChangesAsync();

            return reviewToUpdate;
        }
    }
}
