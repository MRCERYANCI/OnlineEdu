using Microsoft.AspNetCore.Mvc;

namespace OnlineEdu.PresentationLayer.ViewComponents.BlogPageViewComponents
{
    public class _BlogCommentComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke(int blogId)
        {
            ViewBag.blogId = blogId;
            return View();
        }
    }
}
