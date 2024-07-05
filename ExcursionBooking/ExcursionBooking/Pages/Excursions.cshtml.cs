using ExcursionBooking.Data;
using ExcursionBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ExcursionBooking.Pages
{
    public class ExcursionsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ExcursionsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Tour> Tours { get; set; }
        public IList<Category> Categories { get; set; }
        public IDictionary<int, List<int>> TourToCategoryMap { get; set; }

        public async Task OnGetAsync()
        {
            Tours = await _context.Tours.ToListAsync();
            Categories = await _context.Categories.ToListAsync();

            var tourToCategories = await _context.TourToCategories.ToListAsync();
            TourToCategoryMap = tourToCategories
                .GroupBy(tc => tc.TourId)
                .ToDictionary(g => g.Key, g => g.Select(tc => tc.CategoryId).ToList());
        }

        public async Task<IActionResult> OnPostFilterToursAsync([FromBody] int categoryId)
        {
            try
            {
                var tourIdsInCategory = await _context.TourToCategories
                    .Where(tc => tc.CategoryId == categoryId)
                    .Select(tc => tc.TourId)
                    .ToListAsync();

                var filteredTours = await _context.Tours
                    .Where(t => tourIdsInCategory.Contains(t.Id))
                    .ToListAsync();

                // Повертаємо частковий вигляд з відфільтрованими турами
                return Partial("_ExcursionsPartial", Tuple.Create(filteredTours.AsEnumerable(), TourToCategoryMap));
            }
            catch (Exception ex)
            {
                // Логування помилки
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
