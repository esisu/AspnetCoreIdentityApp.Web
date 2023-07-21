using AspnetCoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AspnetCoreIdentityApp.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using AspnetCoreIdentityApp.Web.Extensions;
using Microsoft.EntityFrameworkCore;
using AspnetCoreIdentityApp.Web.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspnetCoreIdentityApp.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;

        private readonly IEmailService _emailService;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
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
            if (!ModelState.IsValid)
            {
                return View();
            }

            returnUrl = returnUrl ?? Url.Action("Index", "Home");

            var hasUser = await _userManager.FindByEmailAsync(request.Email);

            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Email veya Şifre Yanlış");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(hasUser, request.Password, request.RememberMe, true);

            if (result.Succeeded)
            {
                return Redirect(returnUrl);
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModalErrorList(new List<string>() { "Hesabınız kilitlenmiştir. 3 Dakika boyunca giriş yapamazsınız." });
                return View();
            }

            ModelState.AddModalErrorList(new List<string> { $"Email veya Şifre Yanlış.", $"Başarısız giriş sayısı : {await _userManager.GetAccessFailedCountAsync(hasUser)}" });

            return View();
        }
        
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel request)
        {
            AppUser hasUser = await _userManager.FindByEmailAsync(request.Email);
            
            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Bu Email Adresine ait kullanıcı bulunanmamıştır");
                return View();
            }

            string passwordForgetToken = await _userManager.GeneratePasswordResetTokenAsync(hasUser);

            string passwordForgetLink = Url.Action("ResetPassword", "Home",
                new { userId = hasUser.Id, token = passwordForgetToken }, HttpContext.Request.Scheme);
            //örnek link : https://localhost:7257?userId=123123&token=12312d21d12

            //Email Service

            await _emailService.SendForgetPasswordEmail(passwordForgetLink, hasUser.Email);
            
            TempData["SuccessMessage"] = "Şifre yenileme linki eposta adresinize gönderilmiştir.";

            return RedirectToAction(nameof(ForgetPassword));
        }
        
        public async Task<IActionResult> ResetPassword(string userId, string token)
        {
            TempData["userId"] = userId;

            TempData["token"] = token;

            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel request)
        {
            var userId = TempData["userId"];

            var token = TempData["token"];

            if (userId==null || token==null)
            {
                throw new Exception("Bir hata meydana geldi");
            }

            var hasUser = await _userManager.FindByIdAsync(userId.ToString()!);

            if (hasUser == null)
            {
                ModelState.AddModelError(String.Empty, "Kullanıcı Bulunamadı");
                return View();
            }

            IdentityResult result = await _userManager.ResetPasswordAsync(hasUser, token.ToString()!, request.Password);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Şifreniz yenilenmiştir";
            }

            else
            {
                ModelState.AddModalErrorList(result.Errors.Select(x => x.Description).ToList());

            }

            return View();
        }

    }
}