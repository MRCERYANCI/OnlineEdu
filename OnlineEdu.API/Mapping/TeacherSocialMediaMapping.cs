using AutoMapper;
using OnlineEdu.DtoLayer.Dtos.TeacherSocialMediaDtos;
using OnlineEdu.EntityLayer.Entities;

namespace OnlineEdu.API.Mapping
{
    public class TeacherSocialMediaMapping : Profile
    {
        public TeacherSocialMediaMapping()
        {
            CreateMap<ResultTeacherSocialMediaDto, TeacherSocialMedia>().ReverseMap();
            CreateMap<UpdateTeacherSocialMediaDto, TeacherSocialMedia>().ReverseMap();
            CreateMap<CreateTeacherSocialMediaDto, TeacherSocialMedia>().ReverseMap();
        }
    }
}
