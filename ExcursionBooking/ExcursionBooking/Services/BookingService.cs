using ExcursionBooking.Data;
using ExcursionBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace ExcursionBooking.Services
{
    public class BookingService
    {
        private readonly ApplicationDbContext _context;
        private readonly CartService _cartService;
        private readonly ILogger<BookingService> _logger;

        public BookingService(ApplicationDbContext context, CartService cartService, ILogger<BookingService> logger)
        {
            _context = context;
            _cartService = cartService;
            _logger = logger;
        }

        public async Task<Tour> GetTourAsync(int id)
        {
            _logger.LogInformation("Fetching tour with ID {TourId}", id);
            var tour = await _context.Tours.FirstOrDefaultAsync(m => m.Id == id);

            if (tour == null)
            {
                _logger.LogWarning("Tour with ID {TourId} not found", id);
            }
            else
            {
                _logger.LogInformation("Tour with ID {TourId} found: {TourTitle}", id, tour.Title);
            }

            return tour;
        }

        public async Task<(bool success, int adults, int children)> ProcessStep2Async(int id, int adults, int children)
        {
            _logger.LogInformation("Processing step 2 for tour ID {TourId} with {Adults} adults and {Children} children", id, adults, children);
            var tour = await _context.Tours.FirstOrDefaultAsync(m => m.Id == id);

            if (tour == null)
            {
                _logger.LogWarning("Tour with ID {TourId} not found during step 2 processing", id);
                return (false, 0, 0);
            }
            else if(adults == 0 && children == 0)
            {
                _logger.LogWarning("Zero {Adults} and zero {Children} was commited during step 2 processing", adults, children);
                return (false, 0, 0);
            }

            _logger.LogInformation("Step 2 processing completed successfully for tour ID {TourId}", id);
            return (true, adults, children);
        }

        public async Task<(bool success, string message)> CreateOrderAsync(Order order)
        {
            _logger.LogInformation("Creating order for tour ID {TourId} by user {UserId}", order.TourId, order.UserId);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            await _cartService.AddToCartAsync(order.TourId, order.Adults + order.Children, order.UserId);

            _logger.LogInformation("Order created successfully for tour ID {TourId}", order.TourId);
            return (true, "Order created successfully");
        }
    }
}

