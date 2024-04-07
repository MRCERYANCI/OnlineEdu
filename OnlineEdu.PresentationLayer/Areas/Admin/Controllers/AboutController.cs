using Microsoft.AspNetCore.Mvc;
using OnlineEdu.DtoLayer.Dtos.AboutDtos;
using OnlineEdu.PresentationLayer.Helpers;

namespace OnlineEdu.PresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class AboutController : Controller
    {
        private readonly HttpClient _httpClientFactory = HttpClientInstance.CreateClient();

        public async Task<IActionResult> Index()
        {
            try
            {
                TempData["Controller"] = "Hakkımızda";
                TempData["Action"] = "Hakkımızda Listesi";

                var values = await _httpClientFactory.GetFromJsonAsync<List<ResultAboutDto>>("Abouts");
                return View(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
