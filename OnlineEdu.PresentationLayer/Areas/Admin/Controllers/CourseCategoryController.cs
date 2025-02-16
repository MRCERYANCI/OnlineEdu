using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.BusinessLayer.ValidationRules.CourseCategoryRules;
using OnlineEdu.DtoLayer.Dtos.CourseCategoryDtos;
using OnlineEdu.EntityLayer.Entities;
using OnlineEdu.PresentationLayer.Helpers;
using System.Net.Http;

namespace OnlineEdu.PresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class CourseCategoryController(IMapper _mapper) : Controller
    {
        private readonly HttpClient _httpClientFactory = HttpClientInstance.CreateClient();

        public async Task<IActionResult> Index()
        {
            try
            {
                TempData["Controller"] = "Kurs Kategorileri";
                TempData["Action"] = "Kurs Kategori Listesi";

                var values = await _httpClientFactory.GetFromJsonAsync<List<ResultCourseCategoryDto>>("CourseCategories");
                return View(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        public async Task<IActionResult> DeleteCourseCategory(int id)
        {
            try
            {
                if (id == 0 || id == null)
                {
                    return StatusCode(400);
                }
                else
                {
                    await _httpClientFactory.DeleteAsync($"CourseCategories/{id}");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult CreateCourseCategory()
        {
            TempData["Controller"] = "Kurs Kategorileri";
            TempData["Action"] = "Kurs Kategorisi Ekleme Alanı";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourseCategory(CreateCourseCategoryDto createCourseCategoryDto)
        {
            try
            {
                ModelState.Clear();

                CourseCategoryValidator validationRules = new CourseCategoryValidator();
                ValidationResult validationResult = validationRules.Validate(_mapper.Map<CourseCategory>(createCourseCategoryDto));
                if (validationResult.IsValid)
                {


                    await _httpClientFactory.PostAsJsonAsync("CourseCategories", createCourseCategoryDto);

                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    TempData["Controller"] = "Kurs Kategorileri";
                    TempData["Action"] = "Kurs Kategorisi Ekleme Alanı";

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
        public async Task<IActionResult> GetCourseCategory(int id)
        {
            try
            {
                TempData["Controller"] = "Kurs Kategorileri";
                TempData["Action"] = "Kurs Kategorisi Güncelleme Alanı";

                var values = await _httpClientFactory.GetFromJsonAsync<UpdateCourseCategoryDto>($"CourseCategories/{id}");
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
        public async Task<IActionResult> GetCourseCategory(UpdateCourseCategoryDto updateCourseCategoryDto)
        {
            try
            {
                ModelState.Clear();

                CourseCategoryValidator validationRules = new CourseCategoryValidator();
                ValidationResult validationResult = validationRules.Validate(_mapper.Map<CourseCategory>(updateCourseCategoryDto));

                if (validationResult.IsValid)
                {
                    await _httpClientFactory.PutAsJsonAsync("CourseCategories", updateCourseCategoryDto);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var values = await _httpClientFactory.GetFromJsonAsync<UpdateCourseCategoryDto>($"CourseCategories/{updateCourseCategoryDto.CourseCategoryId}");
                    if (values != null)
                    {
                        TempData["Controller"] = "Kurs Kategorileri";
                        TempData["Action"] = "Kurs Kategorisi Güncelleme Alanı";

                        validationResult.Errors.ForEach(x =>
                        {
                            ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                        });

                        return View(values);
                    }
                    else
                        return StatusCode(400, $"Sunucuda {updateCourseCategoryDto.CourseCategoryId} Numaralı Id'ye Göre Veri Bulunamadı");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        public async Task<IActionResult> ShowOnHome(int id)
        {
            var response = await _httpClientFactory.GetAsync($"CourseCategories/ShowOnHome/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public async Task<IActionResult> DontShowOnHome(int id)
        {
            var response = await _httpClientFactory.GetAsync($"CourseCategories/DontShowOnHome/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
    }
}
