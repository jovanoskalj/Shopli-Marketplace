using EShop.Domain.Domain_Models;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace EShop.Service.Implementation
{
    public class WishlistService : IWishlistService
    {
        private readonly IRepository<Wishlist> _wishlistRepository;

        public WishlistService(IRepository<Wishlist> wishlistRepository)
        {
            _wishlistRepository = wishlistRepository;
        }

        public async Task<List<Wishlist>> GetUserWishlistAsync(string userId)
        {
            return await _wishlistRepository.GetAll()
                .Where(w => w.UserId == userId)
                .Include(w => w.Product)
                    .ThenInclude(p => p!.ProductImages)
                .Include(w => w.Product)
                    .ThenInclude(p => p!.Reviews)
                .OrderByDescending(w => w.AddedDate)
                .ToListAsync();
        }

        public async Task<Wishlist> AddToWishlistAsync(string userId, Guid productId)
        {
            // Check if already in wishlist
            var existingItem = await _wishlistRepository.GetAll()
                .FirstOrDefaultAsync(w => w.UserId == userId && w.ProductId == productId);

            if (existingItem != null)
                return existingItem;

            var wishlistItem = new Wishlist
            {
                UserId = userId,
                ProductId = productId,
                AddedDate = DateTime.UtcNow
            };

            return _wishlistRepository.Insert(wishlistItem);
        }

        public async Task RemoveFromWishlistAsync(string userId, Guid productId)
        {
            var wishlistItem = await _wishlistRepository.GetAll()
                .FirstOrDefaultAsync(w => w.UserId == userId && w.ProductId == productId);

            if (wishlistItem != null)
            {
                _wishlistRepository.Delete(wishlistItem);
            }
        }

        public async Task<bool> IsInWishlistAsync(string userId, Guid productId)
        {
            return await _wishlistRepository.GetAll()
                .AnyAsync(w => w.UserId == userId && w.ProductId == productId);
        }

        public async Task ClearWishlistAsync(string userId)
        {
            var wishlistItems = await _wishlistRepository.GetAll()
                .Where(w => w.UserId == userId)
                .ToListAsync();

            foreach (var item in wishlistItems)
            {
                _wishlistRepository.Delete(item);
            }
        }

        public async Task<int> GetWishlistCountAsync(string userId)
        {
            return await _wishlistRepository.GetAll()
                .CountAsync(w => w.UserId == userId);
        }
    }
}
