using Microsoft.AspNetCore.Mvc;
using OnlineEdu.DtoLayer.Dtos.ContactDtos;
using OnlineEdu.PresentationLayer.Helpers;

namespace OnlineEdu.PresentationLayer.ViewComponents.ContactPageViewComponents
{
    public class _ContactMapComponentPartial : ViewComponent
    {
        private readonly HttpClient _httpClientFactory = HttpClientInstance.CreateClient();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _httpClientFactory.GetFromJsonAsync<ResultContactDto>("Contacts/1");
            return View(values);
        }
    }
}
