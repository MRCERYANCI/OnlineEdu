using Microsoft.AspNetCore.Mvc;
using OnlineEdu.DtoLayer.Dtos.BlogDtos;
using OnlineEdu.PresentationLayer.Helpers;

namespace OnlineEdu.PresentationLayer.ViewComponents.BlogPageViewComponents
{
    public class _BlogByCategoryGetAllComponentPartial : ViewComponent
    {

        private readonly HttpClient _httpClientFactory = HttpClientInstance.CreateClient();
        public async Task<IViewComponentResult> InvokeAsync(int categoryId)
        {
            var values = await _httpClientFactory.GetFromJsonAsync<List<ResultBlogDto>>($"Blogs/GetBlogsByCategory/{categoryId}");
            return View(values);
        }
    }
}

