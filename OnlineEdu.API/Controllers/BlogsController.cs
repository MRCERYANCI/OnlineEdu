using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.BusinessLayer.Abstract;
using OnlineEdu.BusniessLayer.Abstract;
using OnlineEdu.DtoLayer.Dtos.BlogDtos;
using OnlineEdu.EntityLayer.Entities;

namespace OnlineEdu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController(IBlogService _genericService, IBlogService _blogService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> BlogGettAll()
        {
            return Ok(_mapper.Map<List<ResultBlogDto>>(await _genericService.TListBlogsWithCategories()));
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

        [HttpGet("ListBlogsWithCategoriesByUser/{appUserId}")]
        public async Task<IActionResult> ListBlogsWithCategoriesByUser(int appUserId)
        {
            return Ok(_mapper.Map<List<ResultBlogDto>>(await _genericService.TListBlogsWithCategoriesByUser(appUserId)));
        }


        [HttpGet("GetLastFourBlogs")]
        public async Task<IActionResult> GetLastFourBlogs()
        {
            return Ok(_mapper.Map<List<ResultBlogDto>>(await _blogService.TGetLastFourBlogs()));
        }

 
        [HttpGet("GetBlogCount")]
        public async Task<IActionResult> GetBlogCount()
        {
            return Ok(await _blogService.TGetBlogCount());
        }


        [HttpGet("GetBlogDetailsWithUser/{id}")]
        public async Task<IActionResult> GetBlogDetailsWithUser(string id)
        {
            return Ok(_mapper.Map<ResultBlogDto>(await _blogService.TGetBlogDetailsWithUser(id)));
        }


        [HttpGet("GetBlogsByCategory/{categoryId}")]
        public async Task<IActionResult> GetBlogsByCategory(int categoryId)
        {
            return Ok(_mapper.Map<List<ResultBlogDto>>(await _blogService.TGetBlogsByCategory(categoryId)));
        }

 
        [HttpGet("SearchBlogPosts/{query}")]
        public async Task<IActionResult> SearchBlogPosts(string query)
        {
            return Ok(_mapper.Map<List<ResultBlogDto>>(await _blogService.TSearchBlogPosts(query)));
        }


        [HttpGet("GetFilteredBlogs/{blogId}")]
        public async Task<IActionResult> GetFilteredBlogs(int blogId)
        {
            return Ok(_mapper.Map<UpdateBlogDto>(await _genericService.TGetFilteredAsync(x=>x.BlogId == blogId)));
        }
    }
}
