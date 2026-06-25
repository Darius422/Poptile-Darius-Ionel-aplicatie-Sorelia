using BeautyHealthStore.Data;
using BeautyHealthStore.Models;
using BeautyHealthStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace BeautyHealthStore.Pages.Checkout
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly EmailService _emailService;
        private readonly EmailSettings _emailSettings;

        public IndexModel(
            AppDbContext context,
            EmailService emailService,
            IOptions<EmailSettings> emailSettings)
        {
            _context = context;
            _emailService = emailService;
            _emailSettings = emailSettings.Value;
        }

        [BindProperty]
        public Order Order { get; set; } = new();

        public IList<CartItem> CartItems { get; set; } = new List<CartItem>();

        public decimal Total { get; set; }

        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            CartItems = await _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            Total = CartItems.Sum(c => (c.Product?.Price ?? 0) * c.Quantity);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cartItems = await _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            if (!cartItems.Any())
            {
                return RedirectToPage("/Cart/Index");
            }

            if (!ModelState.IsValid)
            {
                CartItems = cartItems;
                Total = cartItems.Sum(c => (c.Product?.Price ?? 0) * c.Quantity);
                return Page();
            }

            Order.OrderDate = DateTime.Now;
            Order.Status = "Plasată";
            Order.UserId = userId ?? string.Empty;
            Order.TotalPrice = cartItems.Sum(c => (c.Product?.Price ?? 0) * c.Quantity);

            foreach (var item in cartItems)
            {
                if (item.Product != null)
                {
                    if (item.Product.Stock < item.Quantity)
                    {
                        ModelState.AddModelError(string.Empty,
                            $"Stoc insuficient pentru produsul {item.Product.Name}.");

                        CartItems = cartItems;
                        Total = cartItems.Sum(c => (c.Product?.Price ?? 0) * c.Quantity);

                        return Page();
                    }

                    Order.OrderItems.Add(new OrderItem
                    {
                        ProductId = item.ProductId,
                        ProductName = item.Product.Name,
                        Quantity = item.Quantity,
                        Price = item.Product.Price
                    });

                    item.Product.Stock -= item.Quantity;
                }
            }

            _context.Orders.Add(Order);
            _context.CartItems.RemoveRange(cartItems);

            await _context.SaveChangesAsync();

            string emailBody =
                $"Bună, {Order.CustomerName}!\n\n" +
                $"Comanda ta cu numărul #{Order.Id} a fost plasată cu succes pe Sorelia.\n" +
                $"Total comandă: {Order.TotalPrice} lei\n" +
                $"Adresă livrare: {Order.Address}\n\n" +
                $"Îți mulțumim că ai ales Sorelia!";

            try
            {
                await _emailService.SendEmailAsync(
                    _emailSettings.SmtpServer,
                    _emailSettings.SmtpPort,
                    _emailSettings.SenderEmail,
                    _emailSettings.SenderPassword,
                    Order.CustomerEmail,
                    "Confirmare comandă Sorelia",
                    emailBody);
            }
            catch (Exception)
            {
                // Comanda a fost salvată cu succes.
                // Trimiterea e-mailului de confirmare nu a reușit,
                // dar procesul de comandă continuă normal.
            }

            return RedirectToPage("/Checkout/Success", new { id = Order.Id });
        }
    }
}