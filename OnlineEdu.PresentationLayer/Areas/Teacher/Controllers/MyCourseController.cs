using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineEdu.BusinessLayer.ValidationRules.CourseRules;
using OnlineEdu.DtoLayer.Dtos.CourseCategoryDtos;
using OnlineEdu.DtoLayer.Dtos.CourseDtos;
using OnlineEdu.DtoLayer.Dtos.CourseVideoDtos;
using OnlineEdu.EntityLayer.Entities;
using OnlineEdu.PresentationLayer.Helpers;
using OnlineEdu.PresentationLayer.Services;
using OnlineEdu.PresentationLayer.Services.TokenServices;
using System.Net.Http;

namespace OnlineEdu.PresentationLayer.Areas.Teacher.Controllers
{
    [Authorize(Roles = "Teacher")]
    [Area("Teacher")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class MyCourseController(UserManager<AppUser> _userManager, IMapper _mapper, ITokenService _tokenService) : Controller
    {
        private readonly HttpClient _httpClientFactory = HttpClientInstance.CreateClient();

        public async Task CourseCategoryDropdown()
        {
            var courseCategoryList = await _httpClientFactory.GetFromJsonAsync<List<ResultCourseCategoryDto>>("CourseCategories");

            if (courseCategoryList is not null)
            {
                List<SelectListItem> courseCategorySelectListItems = (from x in courseCategoryList
                                                                      select new SelectListItem
                                                                      {
                                                                          Text = x.CourseCategoryName,
                                                                          Value = x.CourseCategoryId.ToString()
                                                                      }).ToList();

                if (courseCategorySelectListItems is not null)
                {
                    ViewBag.CourseCategoryList = courseCategorySelectListItems;
                }
            }
        }


        public async Task<IActionResult> Index()
        {
            try
            {
                TempData["Controller"] = "Kurslar";
                TempData["Action"] = "Kurs Listesi";

                //var user = await _userManager.FindByNameAsync(User.Identity.Name);

                var values = await _httpClientFactory.GetFromJsonAsync<List<ResultCourseDto>>($"Course/ListCourseWithCategoriesAndTeacher/{_tokenService.GetUserId}");
                return View(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateCourse()
        {
            TempData["Controller"] = "Kurslar";
            TempData["Action"] = "Yeni Kurs Ekleme Alanı";

            await CourseCategoryDropdown();

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> CreateCourse(CreateCourseDto createCourseDto, IFormFile courseResim)
        {
            try
            {
                ModelState.Clear();

                CourseValidator validationRules = new CourseValidator();
                ValidationResult validationResult = validationRules.Validate(_mapper.Map<Course>(createCourseDto));
                if (validationResult.IsValid)
                {
                    if (courseResim is not null)
                    {
                        var user = await _userManager.FindByNameAsync(User.Identity.Name);

                        createCourseDto.ImageUrl = FileService.FileSaveToServer(courseResim, "wwwroot/Images/CourseImages/");
                        createCourseDto.AppUserId = _tokenService.GetUserId;

                        await _httpClientFactory.PostAsJsonAsync("Course", createCourseDto);

                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    await CourseCategoryDropdown();

                    TempData["Controller"] = "Kurslar";
                    TempData["Action"] = "Yeni Kurs Ekleme Alanı";

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

        public async Task<IActionResult> DeleteCourse(int id)
        {
            try
            {
                if (id == 0 || id == null)
                {
                    return StatusCode(400);
                }
                else
                {
                    await _httpClientFactory.DeleteAsync($"Course/{id}");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCourse(int id)
        {
            try
            {
                TempData["Controller"] = "Kurslar";
                TempData["Action"] = "Kurs Güncelleme Alanı";

                var values = await _httpClientFactory.GetFromJsonAsync<UpdateCourseDto>($"Course/{id}");
                if (values != null)
                {
                    await CourseCategoryDropdown();
                    return View(values);
                }
                else
                    return StatusCode(400);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> GetCourse(UpdateCourseDto updateCourseDto, IFormFile courseResim)
        {
            try
            {
                ModelState.Clear();

                CourseValidator validationRules = new CourseValidator();
                ValidationResult validationResult = validationRules.Validate(_mapper.Map<Course>(updateCourseDto));

                if (validationResult.IsValid)
                {
                    if (courseResim is not null)
                    {
                        updateCourseDto.ImageUrl = FileService.FileSaveToServer(courseResim, "wwwroot/Images/CourseImages/");
                    }
                    await _httpClientFactory.PutAsJsonAsync("Course", updateCourseDto);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var values = await _httpClientFactory.GetFromJsonAsync<UpdateCourseDto>($"Course/{updateCourseDto.CourseId}");
                    if (values != null)
                    {
                        TempData["Controller"] = "Kurslar";
                        TempData["Action"] = "Kurs Güncelleme Alanı";

                        await CourseCategoryDropdown();

                        validationResult.Errors.ForEach(x =>
                        {
                            ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                        });

                        return View(values);
                    }
                    else
                        return StatusCode(400, $"Sunucuda {updateCourseDto.CourseId} Numaralı Id'ye Göre Veri Bulunamadı");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        public async Task<IActionResult> CourseVideo(int id)
        {
            TempData["Controller"] = "Kurslar";
            TempData["Action"] = "Kurs Vidoları";

            ViewBag.Id = id;

            var values = await _httpClientFactory.GetFromJsonAsync<List<ResultCourseVideoDto>>($"CourseVideos/list?courseId={id}");
            return View(values);
        }

        public IActionResult CreateCourseVideo(int id)
        {
            TempData["Controller"] = "Kurslar";
            TempData["Action"] = "Kurs Videosu Ekleme Alanı";
            TempData["CourseId"] = id;

            return View();
        }
    }
}
