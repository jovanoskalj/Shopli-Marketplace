using EShop.Domain.Domain_Models;

namespace EShop.Service.Interface
{
    public interface IWishlistService
    {
        Task<List<Wishlist>> GetUserWishlistAsync(string userId);
        Task<Wishlist> AddToWishlistAsync(string userId, Guid productId);
        Task RemoveFromWishlistAsync(string userId, Guid productId);
        Task<bool> IsInWishlistAsync(string userId, Guid productId);
        Task ClearWishlistAsync(string userId);
        Task<int> GetWishlistCountAsync(string userId);
    }
}
