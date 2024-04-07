using Microsoft.AspNetCore.Mvc;
namespace OnlineEdu.PresentationLayer.Areas.Admin.ViewComponents.AdminViewComponents
{
    public class _AdminCreateNewComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
