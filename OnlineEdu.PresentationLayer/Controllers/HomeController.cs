using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.EntityLayer.Entities;

namespace OnlineEdu.PresentationLayer.Controllers
{
    public class HomeController(UserManager<AppUser> _userManager) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
