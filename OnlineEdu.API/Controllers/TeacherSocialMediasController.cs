using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.BusniessLayer.Abstract;
using OnlineEdu.DtoLayer.Dtos.TeacherSocialMediaDtos;
using OnlineEdu.EntityLayer.Entities;

namespace OnlineEdu.API.Controllers
{
    [Authorize(Roles = "Teacher")]
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherSocialMediasController(IGenericService<TeacherSocialMedia> _genericService, IMapper _mapper) : ControllerBase
    {
        [HttpGet("TeacherSocialMediaGettAll/{id}")]
        public async Task<IActionResult> TeacherSocialMediaGettAll(int id)
        {
            return Ok(_mapper.Map<List<ResultTeacherSocialMediaDto>>(await _genericService.TGetFilteredListAsync(x=>x.TeacherId == id)));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdTeacherSocialMedia(int id)
        {
            return Ok(_mapper.Map<ResultTeacherSocialMediaDto>(await _genericService.TGetByIdAsync(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacherSocialMedia(CreateTeacherSocialMediaDto createTeacherSocialMediaDto)
        {
            await _genericService.TCreateAsync(_mapper.Map<TeacherSocialMedia>(createTeacherSocialMediaDto));
            return Ok("Sosyal Medya Alanı Başarıyla Eklenmiştir");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacherSocialMedia(int id)
        {
            await _genericService.TDeleteAsync(id);
            return Ok("Sosyal Medya Alanı Başarıyla Silinmiştir");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTeacherSocialMedia(UpdateTeacherSocialMediaDto updateTeacherSocialMediaDto)
        {
            await _genericService.TUpdateAsync(_mapper.Map<TeacherSocialMedia>(updateTeacherSocialMediaDto));
            return Ok("Sosyal Medya Alanı Başarıyla Güncellenmiştir");
        }
    }
}
