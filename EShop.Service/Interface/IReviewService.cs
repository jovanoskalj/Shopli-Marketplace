using EShop.Domain.Domain_Models;

namespace EShop.Service.Interface
{
    public interface IReviewService
    {
        Task<List<Review>> GetProductReviewsAsync(Guid productId);
        Task<Review?> GetReviewByIdAsync(Guid id);
        Task<Review> CreateReviewAsync(Review review);
        Task<Review> UpdateReviewAsync(Review review);
        Task DeleteReviewAsync(Guid id);
        Task<bool> CanUserReviewProductAsync(string userId, Guid productId);
        Task<double> CalculateAverageRatingAsync(Guid productId);
        void MarkReviewHelpful(Guid reviewId);
    }
}
