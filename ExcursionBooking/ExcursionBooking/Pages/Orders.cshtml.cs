using ExcursionBooking.Data;
using ExcursionBooking.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using OfficeOpenXml;
using Microsoft.AspNetCore.Mvc;

namespace ExcursionBooking.Pages
{
    [Authorize(Roles = "admin")]
    public class OrdersModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public OrdersModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Order> Orders { get; set; }

        public async Task OnGetAsync()
        {
            Orders = await _context.Orders
            .Include(o => o.Tour) 
            .ToListAsync();
        }

        public async Task<IActionResult> OnPostExportAsync()
        {
            Orders = await _context.Orders
                .Include(o => o.Tour)
                .ToListAsync();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Orders");

                worksheet.Cells[1, 1].Value = "Order ID";
                worksheet.Cells[1, 2].Value = "Tour";
                worksheet.Cells[1, 3].Value = "User";
                worksheet.Cells[1, 4].Value = "Adults";
                worksheet.Cells[1, 5].Value = "Children";
                worksheet.Cells[1, 6].Value = "Order Date";
                worksheet.Cells[1, 7].Value = "Phone";
                worksheet.Cells[1, 8].Value = "Email";

                for (int i = 0; i < Orders.Count; i++)
                {
                    var order = Orders[i];
                    worksheet.Cells[i + 2, 1].Value = order.Id;
                    worksheet.Cells[i + 2, 2].Value = order.Tour.Title;
                    worksheet.Cells[i + 2, 3].Value = $"{order.FirstName} {order.LastName}";
                    worksheet.Cells[i + 2, 4].Value = order.Adults;
                    worksheet.Cells[i + 2, 5].Value = order.Children;
                    worksheet.Cells[i + 2, 6].Value = order.OrderDate.ToString("yyyy-MM-dd");
                    worksheet.Cells[i + 2, 7].Value = $"{order.PhoneNumber}";
                    worksheet.Cells[i + 2, 8].Value = $"{order.Email}";
                }

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "exports", "Orders.xlsx");

                var directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                package.SaveAs(new FileInfo(filePath));

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                var content = stream.ToArray();
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = "Orders.xlsx";

                return File(content, contentType, fileName);
            }
        }

    }
}

