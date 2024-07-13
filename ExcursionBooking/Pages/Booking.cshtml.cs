using ExcursionBooking.Data;
using ExcursionBooking.Models;
using ExcursionBooking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ExcursionBooking.Pages
{
    public class BookingModel : PageModel
    {
        private readonly BookingService _bookingService;

        public BookingModel(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [BindProperty]
        public Tour Tour { get; set; }

        [BindProperty]
        public int Adults { get; set; }

        [BindProperty]
        public int Children { get; set; }

        [BindProperty]
        public string FirstName { get; set; }

        [BindProperty]
        public string LastName { get; set; }

        [BindProperty]
        public string PhoneNumber { get; set; }

        [BindProperty]
        public string Email { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Tour = await _bookingService.GetTourAsync(id);

            if (Tour == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostStep2Async(int id, int adults, int children)
        {
            var result = await _bookingService.ProcessStep2Async(id, adults, children);

            if (!result.success)
            {
                return NotFound();
            }

            Adults = result.adults;
            Children = result.children;

            return new JsonResult(new { success = true });
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = userIdClaim.Value;

            var order = new Order
            {
                TourId = Tour.Id,
                Adults = Adults,
                Children = Children,
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber,
                Email = Email,
                OrderDate = DateTime.Now,
                UserId = userId
            };

            var result = await _bookingService.CreateOrderAsync(order);

            if (!result.success)
            {
                return BadRequest(result.message);
            }

            return new JsonResult(new { success = true });
        }
    }

}
