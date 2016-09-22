using MedicalJournals.Identity;
using MedicalJournals.Models.Identity;
using MedicalJournals.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedicalJournals.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _loginManager;
        private readonly RoleManager<JournalRole> _roleManager;


        public AccountController(UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> loginManager,
           RoleManager<JournalRole> roleManager)
        {
            _userManager = userManager;
            _loginManager = loginManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FullName,
                    LastName = model.LastName
                };

                var result = _userManager.CreateAsync(user, model.Password).Result;

                if (result.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync("NormalUser").Result)
                    {
                        var role = new JournalRole {Name = "NormalUser"};

                        var roleResult = _roleManager.CreateAsync(role).Result;
                        if (!roleResult.Succeeded)
                        {
                            ModelState.AddModelError("", "Error while creating role!");
                            return View(model);
                        }
                    }

                    _userManager.AddToRoleAsync(user, "NormalUser").Wait();
                    return RedirectToAction("Login", "Account");
                }
            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _loginManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false).Result;

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid login!");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogOff()
        {
            _loginManager.SignOutAsync().Wait();
            return RedirectToAction("Login", "Account");
        }
    }
}
