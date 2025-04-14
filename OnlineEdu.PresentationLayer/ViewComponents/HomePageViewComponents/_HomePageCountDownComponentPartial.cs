using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.DtoLayer.Dtos.AboutDtos;
using OnlineEdu.EntityLayer.Entities;
using OnlineEdu.PresentationLayer.Helpers;
using OnlineEdu.PresentationLayer.Services.UserService;

namespace OnlineEdu.PresentationLayer.ViewComponents.HomePageViewComponents
{
    public class _HomePageCountDownComponentPartial(IUserService _userService) : ViewComponent
    {
        private readonly HttpClient _httpClientFactory = HttpClientInstance.CreateClient();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.BlogCount = await _httpClientFactory.GetFromJsonAsync<int>("Blogs/GetBlogCount");
            ViewBag.CourseCount = await _httpClientFactory.GetFromJsonAsync<int>("Course/GetCourseCount");
            ViewBag.TeacherCount = await _userService.GetTeacherCountAsync();
            ViewBag.CourseCategoryCount = await _httpClientFactory.GetFromJsonAsync<int>("CourseCategories/GetCourseCategoryCount");
            return View();
        }
    }
}
