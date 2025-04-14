using Microsoft.AspNetCore.Mvc;

namespace OnlineEdu.PresentationLayer.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            TempData["Title-Area"] = "Hakkımızda";
            return View();
        }
    }
}
