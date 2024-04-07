using AutoMapper;
using OnlineEdu.DtoLayer.Dtos.SubscriberDtos;
using OnlineEdu.EntityLayer.Entities;

namespace OnlineEdu.API.Mapping
{
    public class SubscriberMapping : Profile
    {
        public SubscriberMapping()
        {
            CreateMap<ResultSubscriberDto, Subscriber>().ReverseMap();
            CreateMap<UpdateSubscriberDto, Subscriber>().ReverseMap();
            CreateMap<CreateSubscriberDto, Subscriber>().ReverseMap();
        }
    }
}
