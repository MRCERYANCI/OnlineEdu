using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.BusniessLayer.Abstract;
using OnlineEdu.DtoLayer.Dtos.BannerDtos;
using OnlineEdu.EntityLayer.Entities;

namespace OnlineEdu.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class BannersController(IGenericService<Banner> _genericService, IMapper _mapper) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> BannerGettAll()
        {
            return Ok(_mapper.Map<List<ResultBannerDto>>(await _genericService.TGetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdBanner(int id)
        {
            return Ok(_mapper.Map<ResultBannerDto>(await _genericService.TGetByIdAsync(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBanner(CreateBannerDto createBannerDto)
        {
            await _genericService.TCreateAsync(_mapper.Map<Banner>(createBannerDto));
            return Ok("Banner Alanı Başarıyla Eklenmiştir");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBanner(int id)
        {
            await _genericService.TDeleteAsync(id);
            return Ok("Banner Alanı Başarıyla Silinmiştir");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBanner(UpdateBannerDto updateBannerDto)
        {
            await _genericService.TUpdateAsync(_mapper.Map<Banner>(updateBannerDto));
            return Ok("Banner Alanı Başarıyla Güncellenmiştir");
        }
    }
}
