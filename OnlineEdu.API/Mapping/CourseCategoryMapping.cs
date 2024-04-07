using AutoMapper;
using OnlineEdu.DtoLayer.Dtos.CourseCategoryDtos;
using OnlineEdu.EntityLayer.Entities;

namespace OnlineEdu.API.Mapping
{
    public class CourseCategoryMapping : Profile
    {
        public CourseCategoryMapping()
        {
            CreateMap<ResultCourseCategoryDto, CourseCategory>().ReverseMap();
            CreateMap<UpdateCourseCategoryDto, CourseCategory>().ReverseMap();
            CreateMap<CreateCourseCategoryDto, CourseCategory>().ReverseMap();
        }
    }
}
