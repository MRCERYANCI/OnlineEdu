using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OnlineEdu.DtoLayer.Dtos.ContactDtos;
using OnlineEdu.DtoLayer.Dtos.MessageDtos;
using OnlineEdu.PresentationLayer.Helpers;
using System.Net.Http;

namespace OnlineEdu.PresentationLayer.Controllers
{
    public class ContactController : Controller
    {
        private readonly HttpClient _httpClientFactory = HttpClientInstance.CreateClient();

        public IActionResult Index()
        {
            TempData["Title-Area"] = "İletişim";
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CreateContactJson(CreateMessageDto createMessageDto)
        {
            try
            {
                var result = await _httpClientFactory.PostAsJsonAsync("Messages", createMessageDto);

                if (result.IsSuccessStatusCode)
                {
                    return Json("200");
                }
                else
                {
                    return Json("400");
                }
            }
            catch (Exception)
            {
                return Json("500");
            }
        }

        public async Task<PartialViewResult> ContactMap()
        {
            return PartialView(await _httpClientFactory.GetFromJsonAsync<ResultContactDto>("Contacts/1"));
        }
    }
}
