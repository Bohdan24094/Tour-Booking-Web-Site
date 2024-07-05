using ExcursionBooking.Data;
using ExcursionBooking.Models;
using ExcursionBooking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExcursionBooking.Pages
{
    public class CartModel : PageModel
    {
        private readonly CartService _cartService;
        private readonly ApplicationDbContext _context;
        public CartModel(CartService cartService, ApplicationDbContext context)
        {
            _cartService = cartService;
            _context = context;
        }
        public IList<CartItem> CartItems { get; set; }
        public Tour Tour { get; set; }
        public async Task OnGetAsync()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            // Перевіряємо, чи користувач залогований
            if (userIdClaim != null)
            {
                var UserId = userIdClaim.Value;

                CartItems = await _cartService.GetCartItemsAsync(UserId);
            }
            else
            {
                // Якщо користувач не залогований, повертаємо порожній список або іншу відповідну обробку
                CartItems = new List<CartItem>();
            }
        }
    }
}
