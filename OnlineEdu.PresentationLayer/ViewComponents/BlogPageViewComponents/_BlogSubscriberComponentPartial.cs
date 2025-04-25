using Microsoft.AspNetCore.Mvc;

namespace OnlineEdu.PresentationLayer.ViewComponents.BlogPageViewComponents
{
    public class _BlogSubscriberComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
