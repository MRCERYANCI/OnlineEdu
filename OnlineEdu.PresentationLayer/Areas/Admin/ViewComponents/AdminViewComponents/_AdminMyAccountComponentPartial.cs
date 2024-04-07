using Microsoft.AspNetCore.Mvc;

namespace OnlineEdu.PresentationLayer.Areas.Admin.ViewComponents.AdminViewComponents
{
    public class _AdminMyAccountComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
