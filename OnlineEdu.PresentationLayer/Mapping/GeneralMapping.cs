using AutoMapper;
using OnlineEdu.DtoLayer.Dtos.AboutDtos;
using OnlineEdu.DtoLayer.Dtos.BannerDtos;
using OnlineEdu.DtoLayer.Dtos.BlogCategoryDtos;
using OnlineEdu.DtoLayer.Dtos.BlogCommentDtos;
using OnlineEdu.DtoLayer.Dtos.BlogDtos;
using OnlineEdu.DtoLayer.Dtos.ContactDtos;
using OnlineEdu.DtoLayer.Dtos.CourseCategoryDtos;
using OnlineEdu.DtoLayer.Dtos.CourseDtos;
using OnlineEdu.DtoLayer.Dtos.RoleDtos;
using OnlineEdu.DtoLayer.Dtos.SocialMediaDto;
using OnlineEdu.DtoLayer.Dtos.SubscriberDtos;
using OnlineEdu.DtoLayer.Dtos.TeacherSocialMediaDtos;
using OnlineEdu.DtoLayer.Dtos.TestimonialDtos;
using OnlineEdu.DtoLayer.Dtos.UserDtos;
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

            CreateMap<UpdateSocialMediaDto, SocialMedia>().ReverseMap();
            CreateMap<CreateSocialMediaDto, SocialMedia>().ReverseMap();

            CreateMap<ResultRoleDto, AppRole>().ReverseMap();
            CreateMap<CreateRoleDto, AppRole>().ReverseMap();
            CreateMap<UpdateRoleDto, AppRole>().ReverseMap();

            CreateMap<ResultTeacherSocialMediaDto, TeacherSocialMedia>().ReverseMap();
            CreateMap<UpdateTeacherSocialMediaDto, TeacherSocialMedia>().ReverseMap();
            CreateMap<CreateTeacherSocialMediaDto, TeacherSocialMedia>().ReverseMap();

            CreateMap<AppUser, ResultUserDto>().ReverseMap();

            CreateMap<ResultTestimonialDto, Testimonial>().ReverseMap();
            CreateMap<CreateTestimonialDto, Testimonial>().ReverseMap();
            CreateMap<UpdateTestimonialDto, Testimonial>().ReverseMap();

            CreateMap<CreateBlogCommentDto, BlogComment>().ReverseMap();
            CreateMap<UpdateBlogCommentDto, BlogComment>().ReverseMap();

            CreateMap<ResultSubscriberDto, Subscriber>().ReverseMap();
            CreateMap<CreateSubscriberDto, Subscriber>().ReverseMap();
            CreateMap<UpdateSubscriberDto, Subscriber>().ReverseMap();
        }
    }
}
    