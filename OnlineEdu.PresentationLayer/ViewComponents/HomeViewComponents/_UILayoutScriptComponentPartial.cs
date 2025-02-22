using Microsoft.AspNetCore.Mvc;

namespace OnlineEdu.PresentationLayer.ViewComponents.HomeViewComponents
{
    public class _UILayoutScriptComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
