using Microsoft.AspNetCore.Mvc;

namespace OnlineEdu.PresentationLayer.Areas.Teacher.ViewComponents.TeacherViewComponents
{
    public class _TeacherHeaderComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
