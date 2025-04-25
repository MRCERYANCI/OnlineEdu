using Microsoft.AspNetCore.Mvc;
using OnlineEdu.DtoLayer.Dtos.BlogCategoryDtos;
using OnlineEdu.DtoLayer.Dtos.BlogCommentDtos;
using OnlineEdu.DtoLayer.Dtos.BlogDtos;
using OnlineEdu.DtoLayer.Dtos.SubscriberDtos;
using OnlineEdu.PresentationLayer.Helpers;

namespace OnlineEdu.PresentationLayer.Controllers
{
    public class BlogController : Controller
    {
        private readonly HttpClient _httpClientFactory = HttpClientInstance.CreateClient();
        public async Task<IActionResult> Index()
        {
            TempData["Title-Area"] = "Bloglar";
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CreateNewsLetterJson(CreateSubscriberDto createSubscriberDto)
        {
            try
            {
                var result = await _httpClientFactory.PostAsJsonAsync("Subscribers", createSubscriberDto);

                if (result.IsSuccessStatusCode)
                {
                    return Json("200");
                }
                else
                {
                    return Json("400");
                }
            }
            catch (Exception)
            {
                return Json("500");
            }
        }

        [HttpGet]
        public async Task<IActionResult> BlogDetails(string id)
        {
            TempData["Title-Area"] = "Blog Detayları";

            var result = await _httpClientFactory.GetFromJsonAsync<ResultBlogDto>($"Blogs/GetBlogDetailsWithUser/{id}");
            return View(result);
        }

        public async Task<IActionResult> BlogsByCategory(int id)
        {
            var categoryName = await _httpClientFactory.GetFromJsonAsync<ResultBlogCategoryDto>($"BlogCategories/{id}");
            TempData["Title-Area"] = categoryName.Name;
            ViewBag.CategoryId = id;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SearchBlogPosts(string query)
        {
            TempData["Title-Area"] = query + " Kelimesi";
            ViewBag.Query = query;
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CreateBlogJson(CreateBlogCommentDto createBlogCommentDto)
        {
            try
            {
                var result = await _httpClientFactory.PostAsJsonAsync("BlogComments", createBlogCommentDto);

                if (result.IsSuccessStatusCode)
                {
                    return Json("200");
                }
                else
                {
                    return Json("400");
                }
            }
            catch (Exception)
            {
                return Json("500");
            }
        }
    }
}
