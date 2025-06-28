using EShop.Domain.Domain_Models;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace EShop.Service.Implementation
{
    public class ReviewService : IReviewService
    {
        private readonly IRepository<Review> _reviewRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Order> _orderRepository;

        public ReviewService(IRepository<Review> reviewRepository, IRepository<Product> productRepository, IRepository<Order> orderRepository)
        {
            _reviewRepository = reviewRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public async Task<List<Review>> GetProductReviewsAsync(Guid productId)
        {
            return await _reviewRepository.GetAll()
                .Where(r => r.ProductId == productId)
                .Include(r => r.User)
                .OrderByDescending(r => r.ReviewDate)
                .ToListAsync();
        }

        public async Task<Review?> GetReviewByIdAsync(Guid id)
        {
            return await _reviewRepository.GetAll()
                .Include(r => r.User)
                .Include(r => r.Product)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Review> CreateReviewAsync(Review review)
        {
            // Check if user has purchased the product
            var hasPurchased = await _orderRepository.GetAll()
                .Where(o => o.OwnerId == review.UserId)
                .SelectMany(o => o.ProductInOrders!)
                .AnyAsync(pio => pio.ProductId == review.ProductId);

            review.IsVerifiedPurchase = hasPurchased;
            review.ReviewDate = DateTime.UtcNow;

            var createdReview = _reviewRepository.Insert(review);

            // Update product average rating
            await UpdateProductAverageRating(review.ProductId);

            return createdReview;
        }

        public async Task<Review> UpdateReviewAsync(Review review)
        {
            var updatedReview = _reviewRepository.Update(review);
            
            // Update product average rating
            await UpdateProductAverageRating(review.ProductId);

            return updatedReview;
        }

        public async Task DeleteReviewAsync(Guid id)
        {
            var review = _reviewRepository.GetById(id);
            if (review != null)
            {
                var productId = review.ProductId;
                _reviewRepository.Delete(review);
                
                // Update product average rating
                await UpdateProductAverageRating(productId);
            }
        }

        public async Task<bool> CanUserReviewProductAsync(string userId, Guid productId)
        {
            // Check if user has already reviewed this product
            var existingReview = await _reviewRepository.GetAll()
                .AnyAsync(r => r.UserId == userId && r.ProductId == productId);

            return !existingReview;
        }

        public async Task<double> CalculateAverageRatingAsync(Guid productId)
        {
            var reviews = await _reviewRepository.GetAll()
                .Where(r => r.ProductId == productId)
                .ToListAsync();

            if (!reviews.Any())
                return 0;

            return Math.Round(reviews.Average(r => r.Rating), 1);
        }

        public void MarkReviewHelpful(Guid reviewId)
        {
            var review = _reviewRepository.GetById(reviewId);
            if (review != null)
            {
                review.HelpfulCount++;
                _reviewRepository.Update(review);
            }
        }

        private async Task UpdateProductAverageRating(Guid productId)
        {
            var product = _productRepository.GetById(productId);
            if (product != null)
            {
                product.Rating = await CalculateAverageRatingAsync(productId);
                _productRepository.Update(product);
            }
        }
    }
}
