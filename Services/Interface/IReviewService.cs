using Ecom.Models;

namespace Ecom.Services.Interface
{
    public interface IReviewService
    {
        IEnumerable<Review> GetReviews();
        IEnumerable<Review> GetReviewsForProduct(Guid productId);
        void AddReview(string userId, Guid productId, string comment, int rating);
    }
}
