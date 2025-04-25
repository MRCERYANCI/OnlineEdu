using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.EntityLayer.Entities;
using OnlineEdu.PresentationLayer.Services.UserService;

namespace OnlineEdu.PresentationLayer.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class TeacherListController(UserManager<AppUser> _userManager) : Controller
    {
        public async Task<IActionResult> Index()
        {
            TempData["Controller"] = "Kulanıcılar";
            TempData["Action"] = "Eğitmen Listesi";

            var teachers = await _userManager.GetUsersInRoleAsync("Teacher");
            return View(teachers);
        }
    }
}
