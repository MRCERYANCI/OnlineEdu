using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.BusniessLayer.ValidationRules.AboutRules;
using OnlineEdu.DtoLayer.Dtos.AboutDtos;
using OnlineEdu.EntityLayer.Entities;
using OnlineEdu.PresentationLayer.Helpers;

namespace OnlineEdu.PresentationLayer.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class AboutController(IMapper _mapper) : Controller
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

        public async Task<IActionResult> DeleteAbout(int id)
        {
            try
            {
                if (id == 0 || id == null)
                {
                    return StatusCode(400);
                }
                else
                {
                    await _httpClientFactory.DeleteAsync($"Abouts/{id}");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult CreateAbout()
        {
            TempData["Controller"] = "Hakkımızda";
            TempData["Action"] = "Hakkımızda Ekleme Alanı";

            return View();
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto, IFormFile Image1, IFormFile Image2)
        {
            try
            {
                ModelState.Clear();

                AboutValidator validationRules = new AboutValidator();
                ValidationResult validationResult = validationRules.Validate(_mapper.Map<About>(createAboutDto));
                if (validationResult.IsValid)
                {
                    if (Image1 != null && Image2 != null)
                    {
                        var newImageName = Guid.NewGuid() + ".webp";
                        var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/AboutImages/", newImageName);
                        var stream = new FileStream(location, FileMode.Create);
                        Image1.CopyTo(stream);
                        createAboutDto.ImageUrl1 = "/Images/AboutImages/" + newImageName;

                        var newImageName1 = Guid.NewGuid() + ".webp";
                        var location1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/AboutImages/", newImageName1);
                        var stream1 = new FileStream(location1, FileMode.Create);
                        Image2.CopyTo(stream1);
                        createAboutDto.ImageUrl2 = "/Images/AboutImages/" + newImageName1;

                        await _httpClientFactory.PostAsJsonAsync("Abouts", createAboutDto);

                        return RedirectToAction(nameof(Index));
                    }

                }
                else
                {
                    TempData["Controller"] = "Hakkımızda";
                    TempData["Action"] = "Hakkımızda Ekleme Alanı";

                    validationResult.Errors.ForEach(x =>
                    {
                        ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAbout(int id)
        {
            try
            {
                TempData["Controller"] = "Hakkımızda";
                TempData["Action"] = "Hakkımızda Güncelleme Alanı";

                var values = await _httpClientFactory.GetFromJsonAsync<UpdateAboutDto>($"Abouts/{id}");
                if (values != null)
                {
                    return View(values);
                }
                else
                    return StatusCode(404, $"Sunucuda Bu Veri Bulunamadı");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> GetAbout(UpdateAboutDto updateAboutDto)
        {
            try
            {
                ModelState.Clear();

                AboutValidator validationRules = new AboutValidator();
                ValidationResult validationResult = validationRules.Validate(_mapper.Map<About>(updateAboutDto));

                if (validationResult.IsValid)
                {
                    await _httpClientFactory.PutAsJsonAsync("Abouts", updateAboutDto);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var values = await _httpClientFactory.GetFromJsonAsync<UpdateAboutDto>($"Abouts/{updateAboutDto.AboutId}");
                    if (values != null)
                    {
                        TempData["Controller"] = "Hakkımızda";
                        TempData["Action"] = "Hakkımızda Güncelleme Alanı";

                        validationResult.Errors.ForEach(x =>
                        {
                            ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                        });

                        return View(values);
                    }
                    else
                        return StatusCode(400, $"Sunucuda {updateAboutDto.AboutId} Numaralı Id'ye Göre Veri Bulunamadı");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
