using BeautyHealthStore.Models;
using BeautyHealthStore.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BeautyHealthStore.Pages.Account
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileModel(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public ApplicationUser? CurrentUser { get; set; }

        public IList<string> Roles { get; set; } = new List<string>();

        public IList<Order> Orders { get; set; } = new List<Order>();

        public async Task OnGetAsync()
        {
            CurrentUser = await _userManager.GetUserAsync(User);

            if (CurrentUser != null)
            {
                Roles = await _userManager.GetRolesAsync(CurrentUser);

                Orders = await _context.Orders
                  .Where(o => o.UserId == CurrentUser.Id)
                  .OrderByDescending(o => o.OrderDate)
                  .ToListAsync();
            }
        }
    }
}
