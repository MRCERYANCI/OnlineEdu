using Microsoft.AspNetCore.Mvc;
using OnlineEdu.DtoLayer.Dtos.BlogDtos;
using OnlineEdu.PresentationLayer.Helpers;

namespace OnlineEdu.PresentationLayer.ViewComponents.BlogPageViewComponents
{
    public class _BlogSearchGetAllComponentPartial : ViewComponent
    {
        private readonly HttpClient _httpClientFactory = HttpClientInstance.CreateClient();

        public async Task<IViewComponentResult> InvokeAsync(string query)
        {
            var result = await _httpClientFactory.GetFromJsonAsync<List<ResultBlogDto>>($"Blogs/SearchBlogPosts/{query}");
            return View(result);
        }
    }
}
