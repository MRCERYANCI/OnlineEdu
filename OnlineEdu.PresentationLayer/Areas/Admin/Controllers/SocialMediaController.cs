using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.BusinessLayer.ValidationRules.SocialMediaRules;
using OnlineEdu.DtoLayer.Dtos.SocialMediaDto;
using OnlineEdu.EntityLayer.Entities;
using OnlineEdu.PresentationLayer.Helpers;

namespace OnlineEdu.PresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class SocialMediaController(IMapper _mapper) : Controller
    {
        private readonly HttpClient _httpClientFactory = HttpClientInstance.CreateClient();

        public async Task<IActionResult> Index()
        {
            try
            {
                TempData["Controller"] = "Sosyal Medya Hesapları";
                TempData["Action"] = "Sosyal Medya Hesapları Listesi";

                var values = await _httpClientFactory.GetFromJsonAsync<List<ResultSocialMediaDto>>("SocialMedias");
                return View(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        public async Task<IActionResult> DeleteSocialMedia(int id)
        {
            try
            {
                if (id == 0 || id == null)
                {
                    return StatusCode(400);
                }
                else
                {
                    await _httpClientFactory.DeleteAsync($"SocialMedias/{id}");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult CreateSocialMedia()
        {
            TempData["Controller"] = "Sosyal Medya Hesapları";
            TempData["Action"] = "Sosyal Medya Hesapları Ekleme Alanı";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSocialMedia(CreateSocialMediaDto createSocialMediaDto)
        {
            try
            {
                ModelState.Clear();

                SocialMediaValidator validationRules = new SocialMediaValidator();
                ValidationResult validationResult = validationRules.Validate(_mapper.Map<SocialMedia>(createSocialMediaDto));
                if (validationResult.IsValid)
                {


                    await _httpClientFactory.PostAsJsonAsync("SocialMedias", createSocialMediaDto);

                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    TempData["Controller"] = "Sosyal Medya Hesapları";
                    TempData["Action"] = "Sosyal Medya Hesapları Ekleme Alanı";

                    validationResult.Errors.ForEach(x =>
                    {
                        ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetSocialMedia(int id)
        {
            try
            {
                TempData["Controller"] = "Sosyal Medya Hesapları";
                TempData["Action"] = "Sosyal Medya Hesapları Güncelleme Alanı";

                var values = await _httpClientFactory.GetFromJsonAsync<UpdateSocialMediaDto>($"SocialMedias/{id}");
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

        [HttpPost]
        public async Task<IActionResult> GetSocialMedia(UpdateSocialMediaDto updateSocialMediaDto)
        {
            try
            {
                ModelState.Clear();

                SocialMediaValidator validationRules = new SocialMediaValidator();
                ValidationResult validationResult = validationRules.Validate(_mapper.Map<SocialMedia>(updateSocialMediaDto));

                if (validationResult.IsValid)
                {
                    await _httpClientFactory.PutAsJsonAsync("SocialMedias", updateSocialMediaDto);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var values = await _httpClientFactory.GetFromJsonAsync<UpdateSocialMediaDto>($"SocialMedias/{updateSocialMediaDto.SocialMediaId}");
                    if (values != null)
                    {
                        TempData["Controller"] = "Sosyal Medya Hesapları";
                        TempData["Action"] = "Sosyal Medya Hesapları Güncelleme Alanı";

                        validationResult.Errors.ForEach(x =>
                        {
                            ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                        });

                        return View(values);
                    }
                    else
                        return StatusCode(400, $"Sunucuda {updateSocialMediaDto.SocialMediaId} Numaralı Id'ye Göre Veri Bulunamadı");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
