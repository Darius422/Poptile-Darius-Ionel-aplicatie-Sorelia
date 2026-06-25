using BeautyHealthStore.Data;
using BeautyHealthStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BeautyHealthStore.Pages.Checkout
{
    public class SuccessModel : PageModel
    {
        private readonly AppDbContext _context;

        public SuccessModel(AppDbContext context)
        {
            _context = context;
        }

        public Order Order { get; set; } = default!;

        public IActionResult OnGet(int id)
        {
            var order = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
                return NotFound();

            Order = order;
            return Page();
        }
    }
}
