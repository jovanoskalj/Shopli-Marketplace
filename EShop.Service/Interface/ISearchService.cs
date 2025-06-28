using EShop.Domain.Domain_Models;

namespace EShop.Service.Interface
{
    public interface ISearchService
    {
        Task<List<Product>> SearchProductsAsync(string searchTerm, Guid? categoryId = null, double? minPrice = null, double? maxPrice = null, int? minRating = null, bool? inStockOnly = null);
        Task<List<Product>> GetRecommendedProductsAsync(string userId, int count = 10);
        Task<List<Product>> GetFeaturedProductsAsync(int count = 8);
        Task<List<Product>> GetNewArrivalsAsync(int count = 8);
        Task<List<Product>> GetTopRatedProductsAsync(int count = 8);
        Task<List<Product>> GetSimilarProductsAsync(Guid productId, int count = 6);
        void IncrementViewCount(Guid productId);
    }
}
