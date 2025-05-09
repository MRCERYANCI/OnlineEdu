﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.BusinessLayer.Abstract;
using OnlineEdu.BusniessLayer.Abstract;
using OnlineEdu.DtoLayer.Dtos.CourseDtos;
using OnlineEdu.EntityLayer.Entities;

namespace OnlineEdu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController(IGenericService<Course> _genericService, ICourseService _courseService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> CourseGettAll()
        {
            return Ok(_mapper.Map<List<ResultCourseDto>>(await _courseService.TListCourseWithCategories()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCourse(int id)
        {
            return Ok(_mapper.Map<ResultCourseDto>(await _genericService.TGetByIdAsync(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CreateCourseDto createCourseDto)
        {
            await _genericService.TCreateAsync(_mapper.Map<Course>(createCourseDto));
            return Ok("Course Alanı Başarıyla Eklenmiştir");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _genericService.TDeleteAsync(id);
            return Ok("Course Alanı Başarıyla Silinmiştir");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCourse(UpdateCourseDto updateCourseDto)
        {
            await _genericService.TUpdateAsync(_mapper.Map<Course>(updateCourseDto));
            return Ok("Course Alanı Başarıyla Güncellenmiştir");
        }

        [HttpGet("ShowOnHome/{courseId}")]
        public async Task<IActionResult> ShowOnHome(int courseId)
        {
            await _courseService.TShowOnHome(courseId);
            return Ok("Kurs Ana Sayfada Gösteriliyor");
        }

        [HttpGet("DontShowOnHome/{courseId}")]
        public async Task<IActionResult> DontShowOnHome(int courseId)
        {
            await _courseService.TDontShowOnHome(courseId);
            return Ok("Kurs Ana Sayfada Gösterilmiyor");
        }

        [HttpGet("RetrieveActiveCourses")]
        public async Task<IActionResult> RetrieveActiveCourses()
        {
            return Ok(_mapper.Map<List<ResultCourseDto>>(await _genericService.TGetFilteredListAsync(x => x.ShowCase == true && x.Status == true)));
        }

        [HttpGet("ListCourseWithCategoriesAndTeacher/{appUserId}")]
        public async Task<IActionResult> ListCourseWithCategoriesAndTeacher(int appUserId)
        {
            return Ok(_mapper.Map<List<ResultCourseDto>>(await _courseService.TListCourseWithCategoriesAndTeacher(appUserId)));
        }

        [HttpGet("GetCourseCount")]
        public async Task<IActionResult> GetCourseCount()
        {
            return Ok(await _courseService.TCountAsync());
        }

        [HttpGet("GetCoursesByCategoryId/{courseId}")]
        public async Task<IActionResult> GetCoursesByCategoryId(int courseId)
        {
            return Ok(_mapper.Map<List<ResultCourseDto>>(await _courseService.TListCourseWithCategories(x=>x.CourseCategoryId == courseId && x.Status == true)));
        }

        [HttpGet("GetTopSixCourses")]
        public async Task<IActionResult> GetTopSixCourses()
        {
            return Ok(await _courseService.TGetTopSixCourses());
        }

        [HttpGet("GetAllCoursesForHomePage")]
        public async Task<IActionResult> GetAllCoursesForHomePage()
        {
            return Ok(await _courseService.TGetAllCoursesForHomePage());
        }
    }
}
