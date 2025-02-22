using Microsoft.AspNetCore.Mvc;

namespace OnlineEdu.PresentationLayer.ViewComponents.HomePageViewComponents
{
    public class _HomePageSliderAreaComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
