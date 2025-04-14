using Microsoft.AspNetCore.Mvc;
using OnlineEdu.DtoLayer.Dtos.BlogCategoryDtos;
using OnlineEdu.DtoLayer.Dtos.BlogDtos;
using OnlineEdu.PresentationLayer.Helpers;

namespace OnlineEdu.PresentationLayer.ViewComponents.BlogPageViewComponents
{
    public class _BlogCategoryListComponentPartial : ViewComponent
    {
        private readonly HttpClient _httpClientFactory = HttpClientInstance.CreateClient();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categoryList = await _httpClientFactory.GetFromJsonAsync<List<ResultBlogCategoryDto>>("BlogCategories/GetCategoriesWithBlogs");

            var blogCategories = (from blogCategory in categoryList select new ResultBlogCategoryWithCountDto
            {
                CategoryName = blogCategory.Name,
                Count = blogCategory.Blogs.Count
            }).ToList();
            return View(blogCategories);
        }
    }
}
