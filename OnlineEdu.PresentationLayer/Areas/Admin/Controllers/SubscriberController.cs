using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.BusinessLayer.ValidationRules.CourseCategoryRules;
using OnlineEdu.BusinessLayer.ValidationRules.SubscribeRules;
using OnlineEdu.DtoLayer.Dtos.AboutDtos;
using OnlineEdu.DtoLayer.Dtos.CourseCategoryDtos;
using OnlineEdu.DtoLayer.Dtos.SubscriberDtos;
using OnlineEdu.EntityLayer.Entities;
using OnlineEdu.PresentationLayer.Helpers;

namespace OnlineEdu.PresentationLayer.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class SubscriberController(IMapper _mapper) : Controller
    {
        private readonly HttpClient _httpClientFactory = HttpClientInstance.CreateClient();

        public async Task<IActionResult> Index()
        {
            try
            {
                TempData["Controller"] = "Aboneler";
                TempData["Action"] = "Abone Listesi";

                var values = await _httpClientFactory.GetFromJsonAsync<List<ResultSubscriberDto>>("Subscribers");
                return View(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        public async Task<IActionResult> DeleteSubscriber(int id)
        {
            try
            {
                if (id == 0 || id == null)
                {
                    return StatusCode(400);
                }
                else
                {
                    await _httpClientFactory.DeleteAsync($"Subscribers/{id}");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSubscriber(int id)
        {
            try
            {
                TempData["Controller"] = "Aboneler";
                TempData["Action"] = "Abone Güncelleme Alanı";

                var values = await _httpClientFactory.GetFromJsonAsync<UpdateSubscriberDto>($"Subscribers/{id}");
                if (values != null)
                {
                    if(values.IsActive)
                        values.IsActive = false;
                    else
                        values.IsActive = true;

                    await _httpClientFactory.PutAsJsonAsync("Subscribers", values);
                    return RedirectToAction(nameof(Index));
                }
                else
                    return StatusCode(404);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

    }
}
