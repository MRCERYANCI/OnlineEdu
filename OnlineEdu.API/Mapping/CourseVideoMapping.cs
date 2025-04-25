using AutoMapper;
using OnlineEdu.DtoLayer.Dtos.CourseVideoDtos;
using OnlineEdu.EntityLayer.Entities;

namespace OnlineEdu.API.Mapping
{
    public class CourseVideoMapping : Profile
    {
        public CourseVideoMapping()
        {
            CreateMap<CreateCourseVideoDto, CourseVideo>().ReverseMap();
            CreateMap<ResultCourseVideoDto, CourseVideo>().ReverseMap();
        }
    }
}
