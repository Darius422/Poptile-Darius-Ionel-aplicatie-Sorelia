using BeautyHealthStore.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace BeautyHealthStore.Pages.Cart
{
    [Authorize]
    public class UpdateQuantityModel : PageModel
    {
        private readonly AppDbContext _context;

        public UpdateQuantityModel(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id, int quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cartItem = _context.CartItems
                .FirstOrDefault(c => c.Id == id && c.UserId == userId);

            if (cartItem != null)
            {
                if (quantity <= 0)
                {
                    _context.CartItems.Remove(cartItem);
                }
                else
                {
                    var product = _context.Products
                        .FirstOrDefault(p => p.Id == cartItem.ProductId);

                    if (product != null && quantity > product.Stock)
                    {
                        TempData["Error"] = $"Stoc insuficient pentru produsul {product.Name}. Stoc disponibil: {product.Stock} bucăți.";
                        return RedirectToPage("/Cart/Index");
                    }

                    cartItem.Quantity = quantity;
                    _context.CartItems.Update(cartItem);
                }

                _context.SaveChanges();
            }

            return RedirectToPage("./Index");
        }
    }
}