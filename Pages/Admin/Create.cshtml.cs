using BeautyHealthStore.Data;
using BeautyHealthStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace BeautyHealthStore.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; } = new();

        public SelectList Categories { get; set; } = default!;

        public void OnGet()
        {
            Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
                return Page();
            }

            _context.Products.Add(Product);
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}