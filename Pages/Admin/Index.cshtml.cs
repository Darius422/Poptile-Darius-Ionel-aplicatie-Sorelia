using BeautyHealthStore.Data;
using BeautyHealthStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BeautyHealthStore.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Product> Products { get; set; } = new List<Product>();

        public int ProductCount { get; set; }

        public int OrderCount { get; set; }

        public int UserCount { get; set; }

        public decimal TotalSales { get; set; }

        public async Task OnGetAsync()
        {
            Products = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();

            ProductCount = await _context.Products.CountAsync();

            OrderCount = await _context.Orders.CountAsync();

            UserCount = await _context.Users.CountAsync();

            TotalSales = await _context.Orders.SumAsync(o => o.TotalPrice);
        }
    }
}