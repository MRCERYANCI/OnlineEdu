using Microsoft.AspNetCore.Mvc;
using OnlineEdu.PresentationLayer.Services.UserService;

namespace OnlineEdu.PresentationLayer.Controllers
{
    public class TeacherController(IUserService _userService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            TempData["Title-Area"] = "Eğitmenlerimiz";
            return View(await _userService.GetAllTeachers());
        }
    }
}
