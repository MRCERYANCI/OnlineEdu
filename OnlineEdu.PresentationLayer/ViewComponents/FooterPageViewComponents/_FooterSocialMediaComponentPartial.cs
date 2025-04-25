using Microsoft.AspNetCore.Mvc;
using OnlineEdu.DtoLayer.Dtos.SocialMediaDto;
using OnlineEdu.PresentationLayer.Helpers;

namespace OnlineEdu.PresentationLayer.ViewComponents.FooterPageViewComponents
{
    public class _FooterSocialMediaComponentPartial : ViewComponent
    {
        private readonly HttpClient _httpClientFactory = HttpClientInstance.CreateClient();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _httpClientFactory.GetFromJsonAsync<List<ResultSocialMediaDto>>("SocialMedias");
            return View(values);
        }
    }
}
