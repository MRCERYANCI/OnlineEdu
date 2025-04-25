using Microsoft.AspNetCore.Mvc;
using OnlineEdu.DtoLayer.Dtos.BlogDtos;
using OnlineEdu.PresentationLayer.Helpers;

namespace OnlineEdu.PresentationLayer.ViewComponents.BlogPageViewComponents
{
    public class _BlogSearchBlogPostsComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
