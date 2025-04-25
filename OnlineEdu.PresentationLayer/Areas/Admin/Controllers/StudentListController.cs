using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.EntityLayer.Entities;

namespace OnlineEdu.PresentationLayer.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class StudentListController(UserManager<AppUser> _userManager) : Controller
    {
        public async Task<IActionResult> Index()
        {
            TempData["Controller"] = "Kulanıcılar";
            TempData["Action"] = "Öğrenci Listesi";

            var students = await _userManager.GetUsersInRoleAsync("Student");
            return View(students);
        }
    }
}
