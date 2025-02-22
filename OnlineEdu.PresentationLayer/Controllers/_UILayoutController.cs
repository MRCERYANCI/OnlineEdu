using Microsoft.AspNetCore.Mvc;

namespace OnlineEdu.PresentationLayer.Controllers
{
    public class _UILayoutController : Controller
    {
        public IActionResult Layout()
        {
            return View();
        }
    }
}
