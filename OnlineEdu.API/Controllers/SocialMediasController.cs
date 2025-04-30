using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.BusniessLayer.Abstract;
using OnlineEdu.DtoLayer.Dtos.SocialMediaDto;
using OnlineEdu.EntityLayer.Entities;

namespace OnlineEdu.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediasController(IGenericService<SocialMedia> _genericService,IMapper _mapper) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> SocialMediaGettAll()
        {
            return Ok(_mapper.Map<List<ResultSocialMediaDto>>(await _genericService.TGetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdSocialMedia(int id)
        {
            return Ok(_mapper.Map<ResultSocialMediaDto>(await _genericService.TGetByIdAsync(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateSocialMedia(CreateSocialMediaDto createSocialMediaDto)
        {
            await _genericService.TCreateAsync(_mapper.Map<SocialMedia>(createSocialMediaDto));
            return Ok("SocialMedia Alanı Başarıyla Eklenmiştir");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSocialMedia(int id)
        {
            await _genericService.TDeleteAsync(id);
            return Ok("SocialMedia Alanı Başarıyla Silinmiştir");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSocialMedia(UpdateSocialMediaDto updateSocialMediaDto)
        {
            await _genericService.TUpdateAsync(_mapper.Map<SocialMedia>(updateSocialMediaDto));
            return Ok("SocialMedia Alanı Başarıyla Güncellenmiştir");
        }
    }
}
