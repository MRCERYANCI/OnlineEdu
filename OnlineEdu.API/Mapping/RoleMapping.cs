using AutoMapper;
using OnlineEdu.DtoLayer.Dtos.CourseVideoDtos;
using OnlineEdu.DtoLayer.Dtos.RoleDtos;
using OnlineEdu.EntityLayer.Entities;

namespace OnlineEdu.API.Mapping
{
    public class RoleMapping : Profile
    {
        public RoleMapping()
        {
            CreateMap<ResultRoleDto, AppRole>().ReverseMap();
            CreateMap<CreateRoleDto, AppRole>().ReverseMap();
        }
    }
}
