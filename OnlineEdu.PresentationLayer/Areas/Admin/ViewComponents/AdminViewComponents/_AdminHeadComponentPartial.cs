using Microsoft.AspNetCore.Mvc;

namespace OnlineEdu.PresentationLayer.Areas.Admin.ViewComponents.AdminViewComponents
{
    public class _AdminHeadComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
