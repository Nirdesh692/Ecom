using Ecom.Data;
using Ecom.Models;
using Ecom.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Services
{
    public class ReviewService:IReviewService
    {
        private readonly ProductDbContext _context;
        public ReviewService(ProductDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Review> GetReviews()
        {
            var reviews = _context.Reviews.Include(u=>u.User).Include(p=>p.Product).ToList();
            return reviews;
        }
        public IEnumerable<Review> GetReviewsForProduct(Guid productId)
        {
                var reviews = _context.Reviews.Where(r=>r.Product.Id == productId).ToList();
            return reviews;
        }
        public void AddReview(string userId, Guid productId, string comment, int rating)
        {
            var review = new Review
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Comment = comment,
                Rating = rating,
                ProductId = productId,
                ReviewDate = DateTime.Now
            };
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }

    }
}
