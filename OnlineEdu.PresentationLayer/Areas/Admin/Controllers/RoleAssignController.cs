using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineEdu.DtoLayer.Dtos.UserDtos;
using OnlineEdu.EntityLayer.Entities;
using OnlineEdu.PresentationLayer.Services.UserService;

namespace OnlineEdu.PresentationLayer.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class RoleAssignController(IUserService _userService, UserManager<AppUser> _userManager, RoleManager<AppRole> _roleManager,IMapper _mapper) : Controller
    {
        public async Task<IActionResult> Index()
        {
            TempData["Controller"] = "Kulanıcılar";
            TempData["Action"] = "Kullanıcılar Listesi";

            var values = await _userService.GetAllUsersAsync();

            var userList = (from x in values select new UsersRoleDto
            {
                Id = x.Id,
                FirsName = x.FirsName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                ProfileImageUrl = x.ProfileImageUrl,
                UserName = x.UserName,
                Roles = _userManager.GetRolesAsync(x).Result.ToList(),

            }).ToList();
            return View(userList);
        }

        [HttpGet]
        public async Task<IActionResult> AssignRole(int id)
        {
            TempData["Controller"] = "Kulanıcılar";
            TempData["Action"] = "Kullanıcı Rol Listesi";

            var user = await _userService.GetUserByIdAsync(id);

            TempData["userId"] = user.Id;

            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);

            List<AssignRoleDto> assignRoleList = new List<AssignRoleDto>();

            foreach (var role in roles)
            {
                assignRoleList.Add(new AssignRoleDto { Id = role.Id, RoleName = role.Name, RoleExist = userRoles.Contains(role.Name) });
            }

            return View(assignRoleList);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AssignRole(List<AssignRoleDto> assignRoleList)
        {
            int userId = (int)TempData["userId"];

            var user = await _userService.GetUserByIdAsync(userId);

            foreach (var item in assignRoleList)
            {
                if (item.RoleExist)
                {
                    await _userManager.AddToRoleAsync(user, item.RoleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                }
            }

            return RedirectToAction("Index");
        }
    }
}
