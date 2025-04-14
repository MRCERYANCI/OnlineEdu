using Microsoft.AspNetCore.Mvc;
using OnlineEdu.DtoLayer.Dtos.TestimonialDtos;
using OnlineEdu.PresentationLayer.Helpers;
using System.Net.Http;

namespace OnlineEdu.PresentationLayer.ViewComponents.HomePageViewComponents
{
    public class _HomePageTestmonialComponentPartial : ViewComponent
    {
        private readonly HttpClient _httpClientFactory = HttpClientInstance.CreateClient();

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _httpClientFactory.GetFromJsonAsync<List<ResultTestimonialDto>>("Testimonials");
            return View(values);
        }
    }
}
