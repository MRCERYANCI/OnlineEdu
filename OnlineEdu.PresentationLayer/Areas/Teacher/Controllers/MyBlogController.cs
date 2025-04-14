using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineEdu.BusinessLayer.ValidationRules.BlogRules;
using OnlineEdu.DtoLayer.Dtos.BlogCategoryDtos;
using OnlineEdu.DtoLayer.Dtos.BlogDtos;
using OnlineEdu.EntityLayer.Entities;
using OnlineEdu.PresentationLayer.Helpers;
using OnlineEdu.PresentationLayer.Services;

namespace OnlineEdu.PresentationLayer.Areas.Teacher.Controllers
{
    [Authorize(Roles = "Teacher")]
    [Area("Teacher")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class MyBlogController(UserManager<AppUser> _userManager, IMapper _mapper) : Controller
    {
        private readonly HttpClient _httpClientFactory = HttpClientInstance.CreateClient();

        public async Task CategoryDropdown()
        {
            var categoryList = await _httpClientFactory.GetFromJsonAsync<List<ResultBlogCategoryDto>>("BlogCategories");
            if (categoryList is not null)
            {
                List<SelectListItem> categorySelectListItems = (from x in categoryList
                                                                select new SelectListItem
                                                                {
                                                                    Text = x.Name,
                                                                    Value = x.BlogCategoryId.ToString()
                                                                }).ToList();

                if (categorySelectListItems is not null)
                {
                    ViewBag.CategoryList = categorySelectListItems;
                }
            }
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                TempData["Controller"] = "Blog";
                TempData["Action"] = "Blog Listesi";

                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                var values = await _httpClientFactory.GetFromJsonAsync<List<ResultBlogDto>>($"Blogs/ListBlogsWithCategoriesByUser/{user.Id}");
                return View(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        public async Task<IActionResult> DeleteBlog(int id)
        {
            try
            {
                if (id == 0 || id == null)
                {
                    return StatusCode(400);
                }
                else
                {
                    await _httpClientFactory.DeleteAsync($"Blogs/{id}");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateBlog()
        {

            TempData["Controller"] = "Blog";
            TempData["Action"] = "Blog Ekleme Alanı";

            await CategoryDropdown();

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> CreateBlog(CreateBlogDto createBlogDto, IFormFile fileName)
        {
            try
            {
                ModelState.Clear();

                BlogValidator validationRules = new BlogValidator();
                ValidationResult validationResult = await validationRules.ValidateAsync(_mapper.Map<Blog>(createBlogDto));
                if (validationResult.IsValid)
                {
                    if (fileName is not null)
                    {
                        var user = await _userManager.FindByNameAsync(User.Identity.Name);
                        createBlogDto.ImageUrl = FileService.FileSaveToServer(fileName, "wwwroot/Images/BlogImages/");
                        createBlogDto.Status = true;
                        createBlogDto.AppUserId = user.Id;
                        await _httpClientFactory.PostAsJsonAsync("Blogs", createBlogDto);

                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    TempData["Controller"] = "Blog";
                    TempData["Action"] = "Blog Ekleme Alanı";

                    await CategoryDropdown();

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
        public async Task<IActionResult> GetBlog(int id)
        {
            try
            {
                TempData["Controller"] = "Blog";
                TempData["Action"] = "Blog Güncelleme Alanı";

                var values = await _httpClientFactory.GetFromJsonAsync<UpdateBlogDto>($"Blogs/{id}");
                if (values != null)
                {
                    await CategoryDropdown();
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
        public async Task<IActionResult> GetBlog(UpdateBlogDto updateBlogDto)
        {
            try
            {
                ModelState.Clear();

                BlogValidator validationRules = new BlogValidator();
                ValidationResult validationResult = validationRules.Validate(_mapper.Map<Blog>(updateBlogDto));

                if (validationResult.IsValid)
                {
                    await _httpClientFactory.PutAsJsonAsync("Blogs", updateBlogDto);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var values = await _httpClientFactory.GetFromJsonAsync<UpdateBlogDto>($"Blogs/{updateBlogDto.BlogId}");
                    if (values != null)
                    {
                        TempData["Controller"] = "Blog";
                        TempData["Action"] = "Blog Güncelleme Alanı";

                        validationResult.Errors.ForEach(x =>
                        {
                            ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                        });

                        return View(values);
                    }
                    else
                        return StatusCode(400, $"Sunucuda {updateBlogDto.BlogId} Numaralı Id'ye Göre Veri Bulunamadı");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
