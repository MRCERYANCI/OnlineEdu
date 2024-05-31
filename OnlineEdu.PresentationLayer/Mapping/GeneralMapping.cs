using AutoMapper;
using OnlineEdu.DtoLayer.Dtos.AboutDtos;
using OnlineEdu.DtoLayer.Dtos.BannerDtos;
using OnlineEdu.EntityLayer.Entities;

namespace OnlineEdu.PresentationLayer.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<ResultAboutDto, About>().ReverseMap();
            CreateMap<UpdateAboutDto, About>().ReverseMap();
            CreateMap<CreateAboutDto, About>().ReverseMap();

            CreateMap<ResultBannerDto, Banner>().ReverseMap();
            CreateMap<UpdateBannerDto, Banner>().ReverseMap();
            CreateMap<CreateBannerDto, Banner>().ReverseMap();
        }
    }
}
