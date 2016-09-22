using MedicalJournals.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedicalJournals.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        
        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync
                                 (HttpContext.User).Result;

            ViewBag.Message = $"Welcome {user.FirstName}!";
            if (_userManager.IsInRoleAsync(user, "NormalUser").Result)
            {
                ViewBag.RoleMessage = "You are a NormalUser.";
            }
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
