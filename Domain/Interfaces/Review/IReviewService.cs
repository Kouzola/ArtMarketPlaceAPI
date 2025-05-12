using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Interfaces.Review
{
    public interface IReviewService
    {
        Task<IEnumerable<Entities.Review>> GetAllReviewsOfAProductAsync(int productId);
        Task<Entities.Review> AddReviewAsync(Entities.Review review);
        Task<Entities.Review> UpdateReviewAsync(Entities.Review review);
        Task<Entities.Review> GetReviewBydIdAsync(int id);
        Task<bool> DeleteReviewAsync(int id);
        Task<Entities.Review> RespondToAReview(int id, string response);
    }
}
