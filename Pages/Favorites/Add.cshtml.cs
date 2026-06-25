using BeautyHealthStore.Data;
using BeautyHealthStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeautyHealthStore.Pages.Favorites
{
    [Authorize]
    public class AddModel : PageModel
    {
        private readonly AppDbContext _context;

        public AddModel(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return RedirectToPage("/Account/Login");
            }

            var alreadyExists = _context.FavoriteItems
                .Any(f => f.ProductId == id && f.UserId == userId);

            if (!alreadyExists)
            {
                var favoriteItem = new FavoriteItem
                {
                    ProductId = id,
                    UserId = userId
                };

                _context.FavoriteItems.Add(favoriteItem);
                _context.SaveChanges();
            }

            return RedirectToPage("./Index");
        }
    }
}