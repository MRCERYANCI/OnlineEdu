using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.BusinessLayer.ValidationRules.BannerRules;
using OnlineEdu.DtoLayer.Dtos.BannerDtos;
using OnlineEdu.EntityLayer.Entities;
using OnlineEdu.PresentationLayer.Helpers;
using OnlineEdu.PresentationLayer.Services;

namespace OnlineEdu.PresentationLayer.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class BannerController(IMapper _mapper) : Controller
    {
        private readonly HttpClient _httpClientFactory = HttpClientInstance.CreateClient();

        public async Task<IActionResult> Index()
        {
            try
            {
                TempData["Controller"] = "Banner";
                TempData["Action"] = "Banner Listesi";

                var values = await _httpClientFactory.GetFromJsonAsync<List<ResultBannerDto>>("Banners");
                return View(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        public async Task<IActionResult> DeleteBanner(int id)
        {
            try
            {
                if (id == 0 || id == null)
                {
                    return StatusCode(400);
                }
                else
                {
                    await _httpClientFactory.DeleteAsync($"Banners/{id}");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult CreateBanner()
        {
            TempData["Controller"] = "Banner";
            TempData["Action"] = "Banner Ekleme Alanı";

            return View();
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> CreateBanner(CreateBannerDto createBannerDto,IFormFile Image)
        {
            try
            {
                ModelState.Clear();

                BannerValidator validationRules = new BannerValidator();
                ValidationResult validationResult = validationRules.Validate(_mapper.Map<Banner>(createBannerDto));
                if (validationResult.IsValid)
                {
                    if (Image != null)
                    {
                        createBannerDto.ImageUrl = FileService.FileSaveToServer(Image, "wwwroot/Images/BannerImages/");

                        await _httpClientFactory.PostAsJsonAsync("Banners", createBannerDto);

                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    TempData["Controller"] = "Banner";
                    TempData["Action"] = "Banner Ekleme Alanı";

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
        public async Task<IActionResult> GetBanner(int id)
        {
            try
            {
                TempData["Controller"] = "Banner";
                TempData["Action"] = "Banner Güncelleme Alanı";

                var values = await _httpClientFactory.GetFromJsonAsync<UpdateBannerDto>($"Banners/{id}");
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
        public async Task<IActionResult> GetBanner(UpdateBannerDto updateBannerDto)
        {
            try
            {
                ModelState.Clear();

                BannerValidator validationRules = new BannerValidator();
                ValidationResult validationResult = validationRules.Validate(_mapper.Map<Banner>(updateBannerDto));

                if (validationResult.IsValid)
                {
                    await _httpClientFactory.PutAsJsonAsync("Banners", updateBannerDto);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var values = await _httpClientFactory.GetFromJsonAsync<UpdateBannerDto>($"Banners/{updateBannerDto.BannerId}");
                    if (values != null)
                    {
                        TempData["Controller"] = "Banner";
                        TempData["Action"] = "Banner Güncelleme Alanı";

                        validationResult.Errors.ForEach(x =>
                        {
                            ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                        });

                        return View(values);
                    }
                    else
                        return StatusCode(400, $"Sunucuda {updateBannerDto.BannerId} Numaralı Id'ye Göre Veri Bulunamadı");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
