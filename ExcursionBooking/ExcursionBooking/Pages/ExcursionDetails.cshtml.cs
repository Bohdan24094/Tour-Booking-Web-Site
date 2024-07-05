using ExcursionBooking.Data;
using ExcursionBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ExcursionBooking.Pages
{
    public class ExcursionDetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ExcursionDetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Tour Tour { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Tour = await _context.Tours.FirstOrDefaultAsync(m => m.Id == id);

            if (Tour == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
