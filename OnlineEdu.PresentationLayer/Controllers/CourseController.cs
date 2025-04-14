using Microsoft.AspNetCore.Mvc;
using OnlineEdu.DtoLayer.Dtos.CourseDtos;
using OnlineEdu.PresentationLayer.Helpers;

namespace OnlineEdu.PresentationLayer.Controllers
{
    public class CourseController : Controller
    {
        private readonly HttpClient _httpClientFactory = HttpClientInstance.CreateClient();

        public async Task<IActionResult> Index()
        {
            TempData["Title-Area"] = "Tüm Kurslar";
            return View(await _httpClientFactory.GetFromJsonAsync<List<ResultCourseDto>>("Course/GetAllCoursesForHomePage"));
        }

        public async Task<IActionResult> GetCourseByCategory(int id)
        {
            var courses = await _httpClientFactory.GetFromJsonAsync<List<ResultCourseDto>>($"Course/GetCoursesByCategoryId/{id}");
            string categoryName = (from x in courses select x.CourseCategory.CourseCategoryName ).FirstOrDefault();
            TempData["Title-Area"] = categoryName + " Kursları";

            if (courses == null)
            {
                ViewBag.Message = "Şu anda listelenecek bir kurs bulunamadı. Lütfen daha sonra tekrar kontrol edin.";
                TempData["Title-Area"] = "Kurs Bulunamdı";
            }

            return View(courses);
        }
    }
}
