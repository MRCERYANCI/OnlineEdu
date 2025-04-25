using Microsoft.AspNetCore.Mvc;
using OnlineEdu.DtoLayer.Dtos.BlogCategoryDtos;
using OnlineEdu.PresentationLayer.Helpers;
using System.Net.Http;

namespace OnlineEdu.PresentationLayer.ViewComponents.BlogPageViewComponents
{
    public class _BlogCategoryTagComponentPartial : ViewComponent
    {
        private readonly HttpClient _httpClientFactory = HttpClientInstance.CreateClient();

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categoryList = await _httpClientFactory.GetFromJsonAsync<List<ResultBlogCategoryDto>>("BlogCategories");
            return View(categoryList);
        }

    }
}
