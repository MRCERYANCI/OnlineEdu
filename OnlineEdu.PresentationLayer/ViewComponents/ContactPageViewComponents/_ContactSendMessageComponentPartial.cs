using Microsoft.AspNetCore.Mvc;
using OnlineEdu.DtoLayer.Dtos.ContactDtos;

namespace OnlineEdu.PresentationLayer.ViewComponents.ContactPageViewComponents
{
    public class _ContactSendMessageComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
