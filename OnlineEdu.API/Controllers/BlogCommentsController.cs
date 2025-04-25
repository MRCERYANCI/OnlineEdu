using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.BusniessLayer.Abstract;
using OnlineEdu.DtoLayer.Dtos.BlogCommentDtos;
using OnlineEdu.EntityLayer.Entities;

namespace OnlineEdu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCommentsController(IGenericService<BlogComment> _genericService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> BlogCommentGettAll()
        {
            return Ok(_mapper.Map<List<ResultBlogCommentDto>>(await _genericService.TGetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdBlogComment(int id)
        {
            return Ok(_mapper.Map<ResultBlogCommentDto>(await _genericService.TGetByIdAsync(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogComment(CreateBlogCommentDto createBlogCommentDto)
        {
            await _genericService.TCreateAsync(_mapper.Map<BlogComment>(createBlogCommentDto));
            return Ok("BlogComment Alanı Başarıyla Eklenmiştir");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogComment(int id)
        {
            await _genericService.TDeleteAsync(id);
            return Ok("BlogComment Alanı Başarıyla Silinmiştir");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBlogComment(UpdateBlogCommentDto updateBlogCommentDto)
        {
            await _genericService.TUpdateAsync(_mapper.Map<BlogComment>(updateBlogCommentDto));
            return Ok("BlogComment Alanı Başarıyla Güncellenmiştir");
        }
    }
}
