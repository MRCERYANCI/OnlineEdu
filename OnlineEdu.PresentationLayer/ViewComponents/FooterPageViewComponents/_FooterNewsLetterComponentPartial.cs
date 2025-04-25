using Microsoft.AspNetCore.Mvc;

namespace OnlineEdu.PresentationLayer.ViewComponents.FooterPageViewComponents
{
    public class _FooterNewsLetterComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
