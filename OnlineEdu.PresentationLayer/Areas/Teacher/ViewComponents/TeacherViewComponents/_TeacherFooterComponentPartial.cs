using Microsoft.AspNetCore.Mvc;

namespace OnlineEdu.PresentationLayer.Areas.Teacher.ViewComponents.TeacherViewComponents
{
    public class _TeacherFooterComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
