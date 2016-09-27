using MedicalJournals.Common.Settings;
using MedicalJournals.Entities.Interfaces;
using MedicalJournals.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MedicalJournals.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppSettings _appSettings;
        private readonly IUnitOfWork _uow;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(IUnitOfWork uow, UserManager<ApplicationUser> userManager, IOptions<AppSettings> appSettings)
        {
            _uow = uow;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }
        
        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;

            if (user != null)
            {
                ViewBag.Message = $"Welcome {user.FirstName}!";
                if (_userManager.IsInRoleAsync(user, "NormalUser").Result)
                {
                    ViewBag.RoleMessage = "You are a NormalUser.";
                }
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

        public IActionResult StatusCodePage()
        {
            return View("~/Views/Shared/StatusCodePage.cshtml");
        }
    }
}
