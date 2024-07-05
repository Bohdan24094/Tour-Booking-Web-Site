using ExcursionBooking.Data;
using ExcursionBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace ExcursionBooking.Services
{
    public class CartService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CartService> _logger;

        public CartService(ApplicationDbContext context, ILogger<CartService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddToCartAsync(int tourId, int quantity, string userId)
        {
            var cartItem = new CartItem
            {
                TourId = tourId,
                Quantity = quantity,
                UserId = userId
            };

            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Added tour ID {TourId} with quantity {Quantity} to cart for user ID {UserId}", tourId, quantity, userId);
        }

        public async Task<IList<CartItem>> GetCartItemsAsync(string userId)
        {
            _logger.LogInformation("Fetching cart items for user ID {UserId}", userId);
            return await _context.CartItems
                .Where(c => c.UserId == userId)
                .Include(c=>c.Tour)
                .ToListAsync();
        }
    }
}

