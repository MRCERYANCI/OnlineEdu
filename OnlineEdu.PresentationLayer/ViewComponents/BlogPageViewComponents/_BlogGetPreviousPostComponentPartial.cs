using Microsoft.AspNetCore.Mvc;
using OnlineEdu.DtoLayer.Dtos.BlogDtos;
using OnlineEdu.PresentationLayer.Helpers;

namespace OnlineEdu.PresentationLayer.ViewComponents.BlogPageViewComponents
{
    public class _BlogGetPreviousPostComponentPartial : ViewComponent
    {
        private readonly HttpClient _httpClientFactory = HttpClientInstance.CreateClient();

        public async Task<IViewComponentResult> InvokeAsync(int blogId)
        {
            try
            {
                blogId -= 1;
                var values = await _httpClientFactory.GetFromJsonAsync<UpdateBlogDto>($"Blogs/GetFilteredBlogs/{blogId}");

                if (values is not null)
                    return View(values);
                else
                {
                    blogId += 1;
                    return View(await _httpClientFactory.GetFromJsonAsync<UpdateBlogDto>($"Blogs/GetFilteredBlogs/{blogId}"));
                }

            }
            catch (Exception message)
            {
                var data = new UpdateBlogDto();
                return View(data);
            }
        }
    }
}
