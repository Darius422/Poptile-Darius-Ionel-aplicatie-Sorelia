using BeautyHealthStore.Data;
using BeautyHealthStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace BeautyHealthStore.Pages.Cart
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return RedirectToPage("/Account/Login");

            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return RedirectToPage("/Products/Index");

            var cartItem = _context.CartItems
                .FirstOrDefault(c => c.ProductId == id && c.UserId == userId);

            var currentQuantity = cartItem?.Quantity ?? 0;

            if (currentQuantity >= product.Stock)
            {
                TempData["Error"] = $"Stoc insuficient pentru produsul {product.Name}.";
                return RedirectToPage("/Cart/Index");
            }

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    ProductId = id,
                    Quantity = 1,
                    UserId = userId
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
                _context.CartItems.Update(cartItem);
            }

            _context.SaveChanges();
            return RedirectToPage("./Index");
        }
    }
}