using AutoMapper;
using OnlineEdu.DtoLayer.Dtos.BlogCommentDtos;
using OnlineEdu.DtoLayer.Dtos.ContactDtos;
using OnlineEdu.EntityLayer.Entities;

namespace OnlineEdu.API.Mapping
{
    public class BlogCommentMapping : Profile
    {
        public BlogCommentMapping()
        {
            CreateMap<ResultBlogCommentDto, BlogComment>().ReverseMap();
            CreateMap<CreateBlogCommentDto, BlogComment>().ReverseMap();
            CreateMap<UpdateBlogCommentDto, BlogComment>().ReverseMap();
        }
    }
}
