using BeautyHealthStore.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace BeautyHealthStore.Pages.Cart
{
    [Authorize]
    public class RemoveModel : PageModel
    {
        private readonly AppDbContext _context;

        public RemoveModel(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cartItem = _context.CartItems
                .FirstOrDefault(c => c.Id == id && c.UserId == userId);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }

            return RedirectToPage("./Index");
        }
    }
}
