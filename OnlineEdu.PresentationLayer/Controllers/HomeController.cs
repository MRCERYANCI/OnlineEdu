using Microsoft.AspNetCore.Mvc;

namespace OnlineEdu.PresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
