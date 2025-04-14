using Microsoft.AspNetCore.Mvc;

namespace OnlineEdu.PresentationLayer.Controllers
{
    public class BlogController : Controller
    {
        public async Task<IActionResult> Index()
        {
            TempData["Title-Area"] = "Bloglar";
            return View();
        }
    }
}
