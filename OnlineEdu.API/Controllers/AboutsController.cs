using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.BusniessLayer.Abstract;
using OnlineEdu.DtoLayer.Dtos.AboutDtos;
using OnlineEdu.EntityLayer.Entities;

namespace OnlineEdu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutsController(IGenericService<About> _genericService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> AboutGettAll()
        {
            return Ok(_mapper.Map<List<ResultAboutDto>>(await _genericService.TGetAllAsync()));
        }

        [HttpGet("GetFirstRecord")]
        public async Task<IActionResult> GetFirstRecord()
        {
            return Ok(_mapper.Map<ResultAboutDto>(await _genericService.TGetFirstRecord(x => x.AboutId)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAbout(int id)
        {
            return Ok(_mapper.Map<ResultAboutDto>(await _genericService.TGetByIdAsync(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {
            await _genericService.TCreateAsync(_mapper.Map<About>(createAboutDto));
            return Ok("About Alanı Başarıyla Eklenmiştir");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAbout(int id)
        {
            await _genericService.TDeleteAsync(id);
            return Ok("About Alanı Başarıyla Silinmiştir");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            await _genericService.TUpdateAsync(_mapper.Map<About>(updateAboutDto));
            return Ok("About Alanı Başarıyla Güncellenmiştir");
        }
    }
}
