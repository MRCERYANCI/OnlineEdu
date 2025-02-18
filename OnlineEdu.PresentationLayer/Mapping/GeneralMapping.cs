using AutoMapper;
using OnlineEdu.DtoLayer.Dtos.AboutDtos;
using OnlineEdu.DtoLayer.Dtos.BannerDtos;
using OnlineEdu.DtoLayer.Dtos.BlogCategoryDtos;
using OnlineEdu.DtoLayer.Dtos.BlogDtos;
using OnlineEdu.DtoLayer.Dtos.ContactDtos;
using OnlineEdu.DtoLayer.Dtos.CourseCategoryDtos;
using OnlineEdu.DtoLayer.Dtos.CourseDtos;
using OnlineEdu.EntityLayer.Entities;

namespace OnlineEdu.PresentationLayer.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<UpdateAboutDto, About>().ReverseMap();
            CreateMap<CreateAboutDto, About>().ReverseMap();

            CreateMap<UpdateBannerDto, Banner>().ReverseMap();
            CreateMap<CreateBannerDto, Banner>().ReverseMap();

            CreateMap<UpdateBlogCategoryDto, BlogCategory>().ReverseMap();
            CreateMap<CreateBlogCategoryDto, BlogCategory>().ReverseMap();

            CreateMap<UpdateBlogDto, Blog>().ReverseMap();
            CreateMap<CreateBlogDto, Blog>().ReverseMap();

            CreateMap<UpdateContactDto, Contact>().ReverseMap();
            CreateMap<CreateContactDto, Contact>().ReverseMap();

            CreateMap<UpdateCourseCategoryDto, CourseCategory>().ReverseMap();
            CreateMap<CreateCourseCategoryDto, CourseCategory>().ReverseMap();

            CreateMap<UpdateCourseDto, Course>().ReverseMap();
            CreateMap<CreateCourseDto, Course>().ReverseMap();
        }
    }
}
