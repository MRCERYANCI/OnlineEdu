using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.BusniessLayer.Abstract;
using OnlineEdu.DtoLayer.Dtos.TestimonialDtos;
using OnlineEdu.EntityLayer.Entities;

namespace OnlineEdu.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialsController(IGenericService<Testimonial> _genericService,IMapper _mapper): ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> TestimonialGettAll()
        {
            return Ok(_mapper.Map<List<ResultTestimonialDto>>(await _genericService.TGetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdTestimonial(int id)
        {
            return Ok(_mapper.Map<ResultTestimonialDto>(await _genericService.TGetByIdAsync(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestimonial(CreateTestimonialDto createTestimonialDto)
        {
            await _genericService.TCreateAsync(_mapper.Map<Testimonial>(createTestimonialDto));
            return Ok("Testimonial Alanı Başarıyla Eklenmiştir");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            await _genericService.TDeleteAsync(id);
            return Ok("Testimonial Alanı Başarıyla Silinmiştir");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialDto updateTestimonialDto)
        {
            await _genericService.TUpdateAsync(_mapper.Map<Testimonial>(updateTestimonialDto));
            return Ok("Testimonial Alanı Başarıyla Güncellenmiştir");
        }
    }
}
