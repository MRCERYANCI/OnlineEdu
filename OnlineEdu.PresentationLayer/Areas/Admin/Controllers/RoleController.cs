using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineEdu.DtoLayer.Dtos.RoleDtos;
using OnlineEdu.EntityLayer.Entities;
using OnlineEdu.PresentationLayer.Helpers;
using OnlineEdu.PresentationLayer.Services.RoleServices;

namespace OnlineEdu.PresentationLayer.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class RoleController(IMapper _mapper) : Controller
    {
        private readonly HttpClient _httpClient = HttpClientInstance.CreateClient();
        public async Task<IActionResult> Index()
        {
            TempData["Controller"] = "Rol";
            TempData["Action"] = "Rol Listesi";

            var values = await _httpClient.GetFromJsonAsync<List<ResultRoleDto>>("Roles/tum-rolleri-listele");
            return View(values);
        }

        public IActionResult CreateRole()
        {
            TempData["Controller"] = "Rol";
            TempData["Action"] = "Rol Ekleme Alanı";

            return View();
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleDto createRoleDto)
        {
            await _httpClient.PostAsJsonAsync("Roles/rol-olustur", createRoleDto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteRole(int id)
        {
            await _httpClient.DeleteAsync($"Roles/rol-sil/{id}");
            return RedirectToAction("Index");
        }
    }
}
