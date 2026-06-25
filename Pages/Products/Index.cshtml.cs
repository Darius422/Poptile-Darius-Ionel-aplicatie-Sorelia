using BeautyHealthStore.Data;
using BeautyHealthStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BeautyHealthStore.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Product> Products { get; set; } = new List<Product>();

        [BindProperty(SupportsGet = true)]
        public string? Section { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Category { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool NewProducts { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool BestSeller { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            if (!string.IsNullOrEmpty(Section))
            {
                query = query.Where(p => p.Section == Section);
            }

            if (!string.IsNullOrEmpty(Category))
            {
                query = query.Where(p => p.Category != null && p.Category.Name == Category);
            }

            if (NewProducts)
            {
                query = query.Where(p => p.IsNew);
            }

            if (BestSeller)
            {
                query = query.Where(p => p.IsBestSeller);
            }

            Products = await query.ToListAsync();
        }
    }
}