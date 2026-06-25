using BeautyHealthStore.Data;
using BeautyHealthStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BeautyHealthStore.Pages.Favorites
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<FavoriteItem> FavoriteItems { get; set; } = new List<FavoriteItem>();

        public async Task OnGetAsync()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            FavoriteItems = await _context.FavoriteItems
                .Include(f => f.Product)
                .ThenInclude(p => p!.Category)
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }
    }
}