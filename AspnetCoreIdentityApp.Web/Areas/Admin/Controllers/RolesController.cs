using AspnetCoreIdentityApp.Web.Areas.Admin.Models;
using AspnetCoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreIdentityApp.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreIdentityApp.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public RolesController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.Select(x => new RoleViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            return View(roles);
        }

        [Authorize(Roles= "Admin,Rol-Yonetimi")]
        public IActionResult RoleCreate()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Rol-Yonetimi")]
        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleCreateViewModel request)
        {
            var result = await _roleManager.CreateAsync(new AppRole() { Name = request.Name });

            if (!result.Succeeded)
            {
                ModelState.AddModalErrorList(result.Errors);
                return View();
            }

            TempData["SuccessMessage"] = "Ekleme tamamlandı";

            return RedirectToAction(nameof(RolesController.Index));
        }

        [Authorize(Roles = "Admin,Rol-Yonetimi")]
        public async Task<IActionResult> RoleUpdate(string id)
        {
            var roletoupdate = await _roleManager.FindByIdAsync(id);

            if (roletoupdate == null)
            {
                throw new Exception("Güncellenecek rol bulunamamıştır");
            }

            return View(new RoleUpdateViewModel()
            {
                Id = roletoupdate.Id,
                Name = roletoupdate.Name
            });
        }

        [Authorize(Roles = "Admin,Rol-Yonetimi")]
        [HttpPost]
        public async Task<IActionResult> RoleUpdate(RoleUpdateViewModel request)
        {
            var roletoupdate = await _roleManager.FindByIdAsync(request.Id);


            if (roletoupdate == null)
            {
                throw new Exception("Güncellenecek rol bulunamamıştır");
            }

            roletoupdate.Name = request.Name;

            var result = await _roleManager.UpdateAsync(roletoupdate);

            if (!result.Succeeded)
            {
                ModelState.AddModalErrorList(result.Errors);
                return View();
            }

            TempData["SuccessMessage"] = "Güncelleme tamamlandı";

            return View();
        }

        [Authorize(Roles = "Admin,Rol-Yonetimi")]
        public async Task<IActionResult> RoleDelete(string id)
        {
            var roletoDelete = await _roleManager.FindByIdAsync(id);

            if (roletoDelete == null)
            {
                throw new Exception("Silinecek rol bulunamamıştır");
            }

            var result = await _roleManager.DeleteAsync(roletoDelete);

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.Select(x => x.Description).First());
            }

            TempData["SuccessMessage"] = "Silme tamamlandı";

            return RedirectToAction(nameof(RolesController.Index));
        }

        public async Task<IActionResult> AssignRoleToUser(string id)
        {
            var currentUser = await _userManager.FindByIdAsync(id);

            ViewBag.userId = id;

            var roles = await _roleManager.Roles.ToListAsync();

            var userRoles = await _userManager.GetRolesAsync(currentUser);

            var roleViewModelList = new List<AssignRoleToUserViewModel>();

            foreach (var role in roles)
            {
                var AssignToRoleUserviewModel = new AssignRoleToUserViewModel() { Id = role.Id, Name = role.Name };

                if (userRoles.Contains(role.Name))
                {
                    AssignToRoleUserviewModel.Exist = true;
                }

                roleViewModelList.Add(AssignToRoleUserviewModel);
            }

            return View(roleViewModelList);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleToUser(string userId, List<AssignRoleToUserViewModel> requestList)
        {

            var user = await _userManager.FindByIdAsync(userId);

            foreach (var role in requestList)
            {
                if (role.Exist)
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
            }

            return RedirectToAction("UserList","Home");
        }

    }
}
