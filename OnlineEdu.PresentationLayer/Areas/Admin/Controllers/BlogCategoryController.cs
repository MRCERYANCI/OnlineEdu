using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.BusinessLayer.ValidationRules.BlogCategoryRules;
using OnlineEdu.DtoLayer.Dtos.BlogCategoryDtos;
using OnlineEdu.EntityLayer.Entities;
using OnlineEdu.PresentationLayer.Helpers;

namespace OnlineEdu.PresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class BlogCategoryController(IMapper _mapper) : Controller
    {
        private readonly HttpClient _httpClientFactory = HttpClientInstance.CreateClient();

        public async Task<IActionResult> Index()
        {
            try
            {
                TempData["Controller"] = "Blog Kategori";
                TempData["Action"] = "Blog Kategori Listesi";

                var values = await _httpClientFactory.GetFromJsonAsync<List<ResultBlogCategoryDto>>("BlogCategories");
                return View(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        public async Task<IActionResult> DeleteBlogCategory(int id)
        {
            try
            {
                if (id == 0 || id == null)
                {
                    return StatusCode(400);
                }
                else
                {
                    await _httpClientFactory.DeleteAsync($"BlogCategories/{id}");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult CreateBlogCategory()
        {
            TempData["Controller"] = "Blog Kategori";
            TempData["Action"] = "Blog Kategorisi Ekleme Alanı";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogCategory(CreateBlogCategoryDto createBlogCategoryDto)
        {
            try
            {
                ModelState.Clear();

                BlogCategoryValidator validationRules = new BlogCategoryValidator();
                ValidationResult validationResult = await validationRules.ValidateAsync(_mapper.Map<BlogCategory>(createBlogCategoryDto));
                if (validationResult.IsValid)
                {
                    createBlogCategoryDto.Status = true;
                    await _httpClientFactory.PostAsJsonAsync("BlogCategories", createBlogCategoryDto);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Controller"] = "Blog Kategori";
                    TempData["Action"] = "Blog Kategorisi Ekleme Alanı";

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
        public async Task<IActionResult> GetBlogCategory(int id)
        {
            try
            {
                TempData["Controller"] = "Blog Kategori";
                TempData["Action"] = "Blog Kategorisi Güncelleme Alanı";

                var values = await _httpClientFactory.GetFromJsonAsync<UpdateBlogCategoryDto>($"BlogCategories/{id}");
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
        public async Task<IActionResult> GetBlogCategory(UpdateBlogCategoryDto updateBlogCategoryDto)
        {
            try
            {
                ModelState.Clear();

                BlogCategoryValidator validationRules = new BlogCategoryValidator();
                ValidationResult validationResult = validationRules.Validate(_mapper.Map<BlogCategory>(updateBlogCategoryDto));

                if (validationResult.IsValid)
                {
                    await _httpClientFactory.PutAsJsonAsync("BlogCategorys", updateBlogCategoryDto);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var values = await _httpClientFactory.GetFromJsonAsync<UpdateBlogCategoryDto>($"BlogCategories/{updateBlogCategoryDto.BlogCategoryId}");
                    if (values != null)
                    {
                        TempData["Controller"] = "Blog Kategori";
                        TempData["Action"] = "Blog Kategorisi Güncelleme Alanı";

                        validationResult.Errors.ForEach(x =>
                        {
                            ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                        });

                        return View(values);
                    }
                    else
                        return StatusCode(400, $"Sunucuda {updateBlogCategoryDto.BlogCategoryId} Numaralı Id'ye Göre Veri Bulunamadı");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
