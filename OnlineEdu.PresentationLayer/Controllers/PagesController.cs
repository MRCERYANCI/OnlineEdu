using Microsoft.AspNetCore.Mvc;

namespace OnlineEdu.PresentationLayer.Controllers
{
    public class PagesController : Controller
    {
        public IActionResult HandleError(int code)
        {
            return View();
        }
    }
}
