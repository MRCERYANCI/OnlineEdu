using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.BusinessLayer.ValidationRules.CourseRules;
using OnlineEdu.BusinessLayer.ValidationRules.TestimonialRules;
using OnlineEdu.DtoLayer.Dtos.CourseDtos;
using OnlineEdu.DtoLayer.Dtos.TestimonialDtos;
using OnlineEdu.EntityLayer.Entities;
using OnlineEdu.PresentationLayer.Helpers;
using OnlineEdu.PresentationLayer.Services;
using System.Net.Http;

namespace OnlineEdu.PresentationLayer.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class TestimonialController(IMapper _mapper) : Controller
    {
        private readonly HttpClient _httpClientFactory = HttpClientInstance.CreateClient();

        public async Task<IActionResult> Index()
        {
            try
            {
                TempData["Controller"] = "Referanslar";
                TempData["Action"] = "Referansla Listesi";

                var values = await _httpClientFactory.GetFromJsonAsync<List<ResultTestimonialDto>>("Testimonials");
                return View(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            try
            {
                if (id == 0 || id == null)
                {
                    return StatusCode(400);
                }
                else
                {
                    await _httpClientFactory.DeleteAsync($"Testimonials/{id}");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateTestimonial()
        {
            TempData["Controller"] = "Referanslar";
            TempData["Action"] = "Yeni Referans Ekleme Alanı";

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> CreateTestimonial(CreateTestimonialDto createTestimonialDto, IFormFile testimonialResim)
        {
            try
            {
                ModelState.Clear();

                TestimonialValidator validationRules = new TestimonialValidator();
                ValidationResult validationResult = validationRules.Validate(_mapper.Map<Testimonial>(createTestimonialDto));
                if (validationResult.IsValid)
                {
                    if (testimonialResim is not null)
                    {
                        createTestimonialDto.ImageUrl = FileService.FileSaveToServer(testimonialResim, "wwwroot/Images/TestimonialImages/");

                        await _httpClientFactory.PostAsJsonAsync("Testimonials", createTestimonialDto);

                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {

                    TempData["Controller"] = "Referanslar";
                    TempData["Action"] = "Yeni Referans Ekleme Alanı";

                    validationResult.Errors.ForEach(x =>
                    {
                        ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetTestimonial(int id)
        {
            try
            {
                TempData["Controller"] = "Referanslar";
                TempData["Action"] = "Referans Güncelleme Alanı";

                var values = await _httpClientFactory.GetFromJsonAsync<UpdateTestimonialDto>($"Testimonials/{id}");
                if (values != null)
                {
                    return View(values);
                }
                else
                    return StatusCode(400);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> GetTestimonial(UpdateTestimonialDto updateTestimonialDto, IFormFile testimonialResim)
        {
            try
            {
                ModelState.Clear();

                TestimonialValidator validationRules = new TestimonialValidator();
                ValidationResult validationResult = validationRules.Validate(_mapper.Map<Testimonial>(updateTestimonialDto));

                if (validationResult.IsValid)
                {
                    if (testimonialResim is not null)
                    {
                        updateTestimonialDto.ImageUrl = FileService.FileSaveToServer(testimonialResim, "wwwroot/Images/TestimonialImages/");
                    }
                    await _httpClientFactory.PutAsJsonAsync("Testimonials", updateTestimonialDto);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var values = await _httpClientFactory.GetFromJsonAsync<UpdateTestimonialDto>($"Testimonials/{updateTestimonialDto.TestimonialId}");
                    if (values != null)
                    {
                        TempData["Controller"] = "Referanslar";
                        TempData["Action"] = "Referans Güncelleme Alanı";


                        validationResult.Errors.ForEach(x =>
                        {
                            ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                        });

                        return View(values);
                    }
                    else
                        return StatusCode(400, $"Sunucuda {updateTestimonialDto.TestimonialId} Numaralı Id'ye Göre Veri Bulunamadı");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
