using BeautyHealthStore.Data;
using BeautyHealthStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace BeautyHealthStore.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _context;

        public DeleteModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        public string? ErrorMessage { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
                return NotFound();

            var product = _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            Product = product;
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
                return RedirectToPage("./Index");

            bool apareInComenzi = _context.OrderItems
                .Any(oi => oi.ProductId == id);

            if (apareInComenzi)
            {
                Product = product;
                ErrorMessage = "Produsul nu poate fi șters deoarece apare în una sau mai multe comenzi. " +
                               "Modificați stocul la 0 pentru a îl scoate din vânzare.";
                return Page();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}