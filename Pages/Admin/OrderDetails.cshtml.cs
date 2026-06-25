using BeautyHealthStore.Data;
using BeautyHealthStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BeautyHealthStore.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class OrderDetailsModel : PageModel
    {
        private readonly AppDbContext _context;

        public OrderDetailsModel(AppDbContext context)
        {
            _context = context;
        }

        public Order Order { get; set; } = default!;

        [BindProperty]
        public string NewStatus { get; set; } = string.Empty;

        public IActionResult OnGet(int id)
        {
            var order = LoadOrder(id);

            if (order == null)
                return NotFound();

            Order = order;
            NewStatus = order.Status;

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == id);

            if (order == null)
                return NotFound();

            order.Status = NewStatus;
            _context.SaveChanges();

            return RedirectToPage("./OrderDetails", new { id = order.Id });
        }

        private Order? LoadOrder(int id)
        {
            return _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.Id == id);
        }
    }
}