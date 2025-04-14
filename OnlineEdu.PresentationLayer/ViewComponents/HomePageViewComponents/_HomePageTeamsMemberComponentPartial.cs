using Microsoft.AspNetCore.Mvc;
using OnlineEdu.PresentationLayer.Helpers;
using OnlineEdu.PresentationLayer.Services.UserService;

namespace OnlineEdu.PresentationLayer.ViewComponents.HomePageViewComponents
{
    public class _HomePageTeamsMemberComponentPartial(IUserService _userService) : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _userService.GetAll4Teachers());
        }
    }
}
