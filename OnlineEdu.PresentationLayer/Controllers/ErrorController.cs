using Microsoft.AspNetCore.Mvc;

namespace OnlineEdu.PresentationLayer.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error_Page(string message)
        {
            ViewBag.Message = message;
            return View();
        }
    }
}
