using AspnetCoreIdentityApp.Web.Areas.Admin.Models;
using AspnetCoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreIdentityApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {

        private readonly UserManager<AppUser> _userManager;

        public HomeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        
        public async Task<IActionResult> Index()
        {
            return View();
        }
        
        public async Task<IActionResult> UserList()
        {
            List<AppUser> users = await _userManager.Users.ToListAsync();
            List<UserViewModel> viewModel = users.Select(x => new UserViewModel()
            {
                Email = x.Email,
                Id = x.Id,
                Name = x.UserName
            }).ToList();
            return View(viewModel);
        }

    }
}
