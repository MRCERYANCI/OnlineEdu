using Microsoft.AspNetCore.Mvc;
using OnlineEdu.DtoLayer.Dtos.CourseCategoryDtos;
using OnlineEdu.PresentationLayer.Helpers;

namespace OnlineEdu.PresentationLayer.ViewComponents.HomePageViewComponents
{
    public class _HomePageCategoriesComponentPartial : ViewComponent
    {
        private readonly HttpClient _httpClientFactory = HttpClientInstance.CreateClient();

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _httpClientFactory.GetFromJsonAsync<List<ResultCourseCategoryDto>>("CourseCategories/RetrieveActiveCategoriesHomePage");
            return View(values);
        }
    }
}
