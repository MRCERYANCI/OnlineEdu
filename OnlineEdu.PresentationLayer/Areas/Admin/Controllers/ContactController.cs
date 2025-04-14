using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.BusinessLayer.ValidationRules.ContactRules;
using OnlineEdu.DtoLayer.Dtos.ContactDtos;
using OnlineEdu.EntityLayer.Entities;
using OnlineEdu.PresentationLayer.Helpers;
using OnlineEdu.PresentationLayer.Services;

namespace OnlineEdu.PresentationLayer.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class ContactController(IMapper _mapper) : Controller
    {
        private readonly HttpClient _httpClientFactory = HttpClientInstance.CreateClient();

        public async Task<IActionResult> Index()
        {
            try
            {
                TempData["Controller"] = "Contact";
                TempData["Action"] = "Contact Listesi";

                var values = await _httpClientFactory.GetFromJsonAsync<List<ResultContactDto>>("Contacts");
                return View(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        public async Task<IActionResult> DeleteContact(int id)
        {
            try
            {
                if (id == 0 || id == null)
                {
                    return StatusCode(400);
                }
                else
                {
                    await _httpClientFactory.DeleteAsync($"Contacts/{id}");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult CreateContact()
        {
            TempData["Controller"] = "Contact";
            TempData["Action"] = "Contact Ekleme Alanı";

            return View();
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
        {
            try
            {
                ModelState.Clear();

                ContactValidator validationRules = new ContactValidator();
                ValidationResult validationResult = validationRules.Validate(_mapper.Map<Contact>(createContactDto));
                if (validationResult.IsValid)
                {


                    await _httpClientFactory.PostAsJsonAsync("Contacts", createContactDto);

                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    TempData["Controller"] = "Contact";
                    TempData["Action"] = "Contact Ekleme Alanı";

                    validationResult.Errors.ForEach(x =>
                    {
                        ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetContact(int id)
        {
            try
            {
                TempData["Controller"] = "Contact";
                TempData["Action"] = "Contact Güncelleme Alanı";

                var values = await _httpClientFactory.GetFromJsonAsync<UpdateContactDto>($"Contacts/{id}");
                if (values != null)
                {
                    return View(values);
                }
                else
                    return StatusCode(404, $"Sunucuda Bu Veri Bulunamadı");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> GetContact(UpdateContactDto updateContactDto)
        {
            try
            {
                ModelState.Clear();

                ContactValidator validationRules = new ContactValidator();
                ValidationResult validationResult = validationRules.Validate(_mapper.Map<Contact>(updateContactDto));

                if (validationResult.IsValid)
                {
                    await _httpClientFactory.PutAsJsonAsync("Contacts", updateContactDto);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var values = await _httpClientFactory.GetFromJsonAsync<UpdateContactDto>($"Contacts/{updateContactDto.ContactId}");
                    if (values != null)
                    {
                        TempData["Controller"] = "Contact";
                        TempData["Action"] = "Contact Güncelleme Alanı";

                        validationResult.Errors.ForEach(x =>
                        {
                            ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                        });

                        return View(values);
                    }
                    else
                        return StatusCode(400, $"Sunucuda {updateContactDto.ContactId} Numaralı Id'ye Göre Veri Bulunamadı");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
