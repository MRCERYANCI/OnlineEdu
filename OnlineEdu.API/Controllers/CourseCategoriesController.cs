using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.BusinessLayer.Abstract;
using OnlineEdu.BusniessLayer.Abstract;
using OnlineEdu.DtoLayer.Dtos.CourseCategoryDtos;
using OnlineEdu.EntityLayer.Entities;

namespace OnlineEdu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseCategoriesController(IGenericService<CourseCategory> _genericService,ICourseCategoryService _courseCategoryService , IMapper _mapper): ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> CourseCategoryGettAll()
        {
            return Ok(_mapper.Map<List<ResultCourseCategoryDto>>(await _genericService.TGetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCourseCategory(int id)
        {
            return Ok(_mapper.Map<ResultCourseCategoryDto>(await _genericService.TGetByIdAsync(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourseCategory(CreateCourseCategoryDto createCourseCategoryDto)
        {
            await _genericService.TCreateAsync(_mapper.Map<CourseCategory>(createCourseCategoryDto));
            return Ok("CourseCategory Alanı Başarıyla Eklenmiştir");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourseCategory(int id)
        {
            await _genericService.TDeleteAsync(id);
            return Ok("CourseCategory Alanı Başarıyla Silinmiştir");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCourseCategory(UpdateCourseCategoryDto updateCourseCategoryDto)
        {
            await _genericService.TUpdateAsync(_mapper.Map<CourseCategory>(updateCourseCategoryDto));
            return Ok("CourseCategory Alanı Başarıyla Güncellenmiştir");
        }

        [HttpGet("DontShowOnHome/{courseCategoryId}")]
        public async Task<IActionResult> DontShowOnHome(int courseCategoryId)
        {
            await _courseCategoryService.TDontShowOnHome(courseCategoryId);

            return Ok();
        }

        [HttpGet("ShowOnHome/{courseCategoryId}")]
        public async Task<IActionResult> ShowOnHome(int courseCategoryId)
        {
            await _courseCategoryService.TShowOnHome(courseCategoryId);

            return Ok();
        }
    }
}
