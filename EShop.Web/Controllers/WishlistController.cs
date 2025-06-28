using EShop.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace EShop.Web.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        // GET: Wishlist
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var wishlistItems = await _wishlistService.GetUserWishlistAsync(userId);
            return View(wishlistItems);
        }

        // POST: Wishlist/Add
        [HttpPost]
        public async Task<IActionResult> Add(Guid productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Json(new { success = false, message = "Please log in to add items to wishlist" });
            }

            try
            {
                await _wishlistService.AddToWishlistAsync(userId, productId);
                return Json(new { success = true, message = "Item added to wishlist" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error adding item to wishlist" });
            }
        }

        // POST: Wishlist/Remove
        [HttpPost]
        public async Task<IActionResult> Remove(Guid productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Json(new { success = false, message = "Please log in" });
            }

            try
            {
                await _wishlistService.RemoveFromWishlistAsync(userId, productId);
                return Json(new { success = true, message = "Item removed from wishlist" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error removing item from wishlist" });
            }
        }

        // GET: Wishlist/Count
        public async Task<IActionResult> Count()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Json(new { count = 0 });
            }

            var count = await _wishlistService.GetWishlistCountAsync(userId);
            return Json(new { count = count });
        }
    }
}
