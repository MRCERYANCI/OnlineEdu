using Microsoft.AspNetCore.Mvc;
using OnlineEdu.DtoLayer.Dtos.CourseDtos;
using OnlineEdu.PresentationLayer.Helpers;

namespace OnlineEdu.PresentationLayer.ViewComponents.HomePageViewComponents
{
    public class _HomePageCourseComponentPartial : ViewComponent
    {
        private readonly HttpClient _httpClientFactory = HttpClientInstance.CreateClient();

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _httpClientFactory.GetFromJsonAsync<List<ResultCourseDto>>("Course/GetTopSixCourses");
            return View(values);
        }
    }
}
