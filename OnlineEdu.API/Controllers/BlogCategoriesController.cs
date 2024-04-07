using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.BusniessLayer.Abstract;
using OnlineEdu.DtoLayer.Dtos.BlogCategoryDtos;
using OnlineEdu.EntityLayer.Entities;

namespace OnlineEdu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCategoriesController(IGenericService<BlogCategory> _genericService,IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> BlogCategoryGettAll()
        {
            return Ok(_mapper.Map<List<ResultBlogCategoryDto>>(await _genericService.TGetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdBlogCategory(int id)
        {
            return Ok(_mapper.Map<ResultBlogCategoryDto>(await _genericService.TGetByIdAsync(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogCategory(CreateBlogCategoryDto createBlogCategoryDto)
        {
            await _genericService.TCreateAsync(_mapper.Map<BlogCategory>(createBlogCategoryDto));
            return Ok("BlogCategory Alanı Başarıyla Eklenmiştir");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogCategory(int id)
        {
            await _genericService.TDeleteAsync(id);
            return Ok("BlogCategory Alanı Başarıyla Silinmiştir");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBlogCategory(UpdateBlogCategoryDto updateBlogCategoryDto)
        {
            await _genericService.TUpdateAsync(_mapper.Map<BlogCategory>(updateBlogCategoryDto));
            return Ok("BlogCategory Alanı Başarıyla Güncellenmiştir");
        }
    }
}
