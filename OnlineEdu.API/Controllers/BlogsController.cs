using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.BusniessLayer.Abstract;
using OnlineEdu.DtoLayer.Dtos.BlogDtos;
using OnlineEdu.EntityLayer.Entities;

namespace OnlineEdu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController(IGenericService<Blog> _genericService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> BlogGettAll()
        {
            return Ok(_mapper.Map<List<ResultBlogDto>>(await _genericService.TGetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdBlog(int id)
        {
            return Ok(_mapper.Map<ResultBlogDto>(await _genericService.TGetByIdAsync(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog(CreateBlogDto createBlogDto)
        {
            await _genericService.TCreateAsync(_mapper.Map<Blog>(createBlogDto));
            return Ok("Blog Alanı Başarıyla Eklenmiştir");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            await _genericService.TDeleteAsync(id);
            return Ok("Blog Alanı Başarıyla Silinmiştir");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBlog(UpdateBlogDto updateBlogDto)
        {
            await _genericService.TUpdateAsync(_mapper.Map<Blog>(updateBlogDto));
            return Ok("Blog Alanı Başarıyla Güncellenmiştir");
        }
    }
}
