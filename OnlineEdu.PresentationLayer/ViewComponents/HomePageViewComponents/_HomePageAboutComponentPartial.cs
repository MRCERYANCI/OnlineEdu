using Microsoft.AspNetCore.Mvc;
using OnlineEdu.DtoLayer.Dtos.AboutDtos;
using OnlineEdu.PresentationLayer.Helpers;

namespace OnlineEdu.PresentationLayer.ViewComponents.HomePageViewComponents
{
    public class _HomePageAboutComponentPartial : ViewComponent
    {
        private readonly HttpClient _httpClientFactory = HttpClientInstance.CreateClient();

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _httpClientFactory.GetFromJsonAsync<ResultAboutDto>("Abouts/GetFirstRecord");
            return View(values);
        }
    }
}
