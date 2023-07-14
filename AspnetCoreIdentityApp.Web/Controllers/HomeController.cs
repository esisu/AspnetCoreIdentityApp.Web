using AspnetCoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AspnetCoreIdentityApp.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using AspnetCoreIdentityApp.Web.Extensions;

namespace AspnetCoreIdentityApp.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel request)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            var identityResult = await _userManager.CreateAsync(new()
            {
                UserName = request.Username,
                PhoneNumber = request.Phone,
                Email = request.Email,
            }, request.ConfirmPassword);

            if (identityResult.Succeeded)
            {
                TempData["SuccessMessage"] = "Üyelik Kayıt işlemi başarı ile gerçekleşmiştir.";

                return RedirectToAction(nameof(HomeController.SignUp));
            }

            ModelState.AddModalErrorList(identityResult.Errors.Select(x => x.Description).ToList());

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel request, string returnUrl = null)
        {

            returnUrl = returnUrl ?? Url.Action("Index", "Home");

            var hasUser = await _userManager.FindByEmailAsync(request.Email);

            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Email veya Şifre Yanlış");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(hasUser, request.Password, request.RememberMe, false);

            if (result.Succeeded)
            {
                return Redirect(returnUrl);
            }

            ModelState.AddModalErrorList(new List<string> { "Email veya Şifre Yanlış" });

            return View();
        }

    }
}