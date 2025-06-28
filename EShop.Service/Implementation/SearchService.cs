using EShop.Domain.Domain_Models;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace EShop.Service.Implementation
{
    public class SearchService : ISearchService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Order> _orderRepository;

        public SearchService(IRepository<Product> productRepository, IRepository<Order> orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public async Task<List<Product>> SearchProductsAsync(string searchTerm, Guid? categoryId = null, double? minPrice = null, double? maxPrice = null, int? minRating = null, bool? inStockOnly = null)
        {
            var query = _productRepository.GetAll()
                .Where(p => p.IsActive)
                .Include(p => p.Category)
                .Include(p => p.Reviews)
                .Include(p => p.ProductImages)
                .AsQueryable();

            // Search term filter
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p => 
                    p.ProductName.Contains(searchTerm) ||
                    p.ProductDescription.Contains(searchTerm) ||
                    p.Brand!.Contains(searchTerm) ||
                    p.Tags!.Contains(searchTerm) ||
                    p.Category!.Name.Contains(searchTerm));
            }

            // Category filter
            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            // Price filters
            if (minPrice.HasValue)
            {
                query = query.Where(p => (p.DiscountedPrice ?? p.ProductPrice) >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => (p.DiscountedPrice ?? p.ProductPrice) <= maxPrice.Value);
            }

            // Rating filter
            if (minRating.HasValue)
            {
                query = query.Where(p => p.Rating >= minRating.Value);
            }

            // Stock filter
            if (inStockOnly == true)
            {
                query = query.Where(p => p.StockQuantity > 0);
            }

            return await query
                .OrderByDescending(p => p.IsFeatured)
                .ThenByDescending(p => p.Rating)
                .ThenBy(p => p.ProductName)
                .ToListAsync();
        }

        public async Task<List<Product>> GetRecommendedProductsAsync(string userId, int count = 10)
        {
            // Get user's purchase history
            var userPurchases = await _orderRepository.GetAll()
                .Where(o => o.OwnerId == userId)
                .SelectMany(o => o.ProductInOrders!)
                .Select(pio => pio.ProductId)
                .Distinct()
                .ToListAsync();

            if (!userPurchases.Any())
            {
                // If no purchase history, return featured products
                return await GetFeaturedProductsAsync(count);
            }

            // Get categories of purchased products
            var purchasedCategories = await _productRepository.GetAll()
                .Where(p => userPurchases.Contains(p.Id))
                .Select(p => p.CategoryId)
                .Distinct()
                .ToListAsync();

            // Recommend products from same categories that user hasn't purchased
            var recommendations = await _productRepository.GetAll()
                .Where(p => p.IsActive && 
                           !userPurchases.Contains(p.Id) && 
                           purchasedCategories.Contains(p.CategoryId) &&
                           p.Rating >= 3.0)
                .Include(p => p.Category)
                .Include(p => p.Reviews)
                .Include(p => p.ProductImages)
                .OrderByDescending(p => p.Rating)
                .ThenByDescending(p => p.ViewCount)
                .Take(count)
                .ToListAsync();

            return recommendations;
        }

        public async Task<List<Product>> GetFeaturedProductsAsync(int count = 8)
        {
            return await _productRepository.GetAll()
                .Where(p => p.IsActive && p.IsFeatured)
                .Include(p => p.Category)
                .Include(p => p.Reviews)
                .Include(p => p.ProductImages)
                .OrderByDescending(p => p.Rating)
                .ThenByDescending(p => p.ViewCount)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Product>> GetNewArrivalsAsync(int count = 8)
        {
            return await _productRepository.GetAll()
                .Where(p => p.IsActive)
                .Include(p => p.Category)
                .Include(p => p.Reviews)
                .Include(p => p.ProductImages)
                .OrderByDescending(p => p.CreatedOn)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Product>> GetTopRatedProductsAsync(int count = 8)
        {
            return await _productRepository.GetAll()
                .Where(p => p.IsActive && p.Rating >= 4.0)
                .Include(p => p.Category)
                .Include(p => p.Reviews)
                .Include(p => p.ProductImages)
                .OrderByDescending(p => p.Rating)
                .ThenByDescending(p => p.ReviewCount)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Product>> GetSimilarProductsAsync(Guid productId, int count = 6)
        {
            var product = _productRepository.GetById(productId);
            if (product == null) return new List<Product>();

            return await _productRepository.GetAll()
                .Where(p => p.IsActive && 
                           p.Id != productId && 
                           (p.CategoryId == product.CategoryId || p.Brand == product.Brand))
                .Include(p => p.Category)
                .Include(p => p.Reviews)
                .Include(p => p.ProductImages)
                .OrderByDescending(p => p.Rating)
                .Take(count)
                .ToListAsync();
        }

        public void IncrementViewCount(Guid productId)
        {
            var product = _productRepository.GetById(productId);
            if (product != null)
            {
                product.ViewCount++;
                _productRepository.Update(product);
            }
        }
    }
}
