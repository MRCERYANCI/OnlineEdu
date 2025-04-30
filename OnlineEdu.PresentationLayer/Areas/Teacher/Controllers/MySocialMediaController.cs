using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.BusinessLayer.ValidationRules.SocialMediaRules;
using OnlineEdu.BusinessLayer.ValidationRules.TeacherSocialMediaRules;
using OnlineEdu.DtoLayer.Dtos.SocialMediaDto;
using OnlineEdu.DtoLayer.Dtos.TeacherSocialMediaDtos;
using OnlineEdu.EntityLayer.Entities;
using OnlineEdu.PresentationLayer.Helpers;
using OnlineEdu.PresentationLayer.Services.TokenServices;

namespace OnlineEdu.PresentationLayer.Areas.Teacher.Controllers
{
    [Authorize(Roles = "Teacher")]
    [Area("Teacher")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class MySocialMediaController(UserManager<AppUser> _userManager, IMapper _mapper,ITokenService _tokenService) : Controller
    {
        private readonly HttpClient _httpClientFactory = HttpClientInstance.CreateClient();

        public async Task<IActionResult> Index()
        {
            try
            {
                TempData["Controller"] = "Sosyal Medyalar";
                TempData["Action"] = "Sosyal Medya Listesi";

                var values = await _httpClientFactory.GetFromJsonAsync<List<ResultTeacherSocialMediaDto>>($"TeacherSocialMedias/TeacherSocialMediaGettAll/{_tokenService.GetUserId}");
                return View(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        public async Task<IActionResult> DeleteTeacherSocialMedia(int id)
        {
            try
            {
                if (id == 0 || id == null)
                {
                    return StatusCode(400);
                }
                else
                {
                    await _httpClientFactory.DeleteAsync($"TeacherSocialMedias/{id}");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public IActionResult CreateTeacherSocialMedia()
        {
            TempData["Controller"] = "Sosyal Medya Hesapları";
            TempData["Action"] = "Sosyal Medya Hesapları Ekleme Alanı";

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> CreateTeacherSocialMedia(CreateTeacherSocialMediaDto createTeacherSocialMedia)
        {
            try
            {
                ModelState.Clear();

                TeacherSocialMediaValidator validationRules = new TeacherSocialMediaValidator();
                ValidationResult validationResult = validationRules.Validate(_mapper.Map<TeacherSocialMedia>(createTeacherSocialMedia));
                if (validationResult.IsValid)
                {
                    createTeacherSocialMedia.TeacherId = _tokenService.GetUserId;
                    await _httpClientFactory.PostAsJsonAsync("TeacherSocialMedias", createTeacherSocialMedia);

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

                var values = await _httpClientFactory.GetFromJsonAsync<UpdateTeacherSocialMediaDto>($"TeacherSocialMedias/{id}");
                if (values != null)
                {
                    return View(values);
                }
                else
                    return StatusCode(404, $"Sunucuda Bu Veri Bulunamadı");
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> GetSocialMedia(UpdateTeacherSocialMediaDto updateTeacherSocialMediaDto)
        {
            try
            {
                ModelState.Clear();

                TeacherSocialMediaValidator validationRules = new TeacherSocialMediaValidator();
                ValidationResult validationResult = validationRules.Validate(_mapper.Map<TeacherSocialMedia>(updateTeacherSocialMediaDto));

                if (validationResult.IsValid)
                {
                    await _httpClientFactory.PutAsJsonAsync("TeacherSocialMedias", updateTeacherSocialMediaDto);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var values = await _httpClientFactory.GetFromJsonAsync<UpdateSocialMediaDto>($"TeacherSocialMedias/{updateTeacherSocialMediaDto.TeacherSocialMediaId}");
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
                        return StatusCode(400, $"Sunucuda {updateTeacherSocialMediaDto.TeacherSocialMediaId} Numaralı Id'ye Göre Veri Bulunamadı");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
