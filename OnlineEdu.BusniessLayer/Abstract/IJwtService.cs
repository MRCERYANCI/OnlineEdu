using OnlineEdu.DtoLayer.Dtos.JwtDtos;
using OnlineEdu.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.BusinessLayer.Abstract
{
    public interface IJwtService
    {
        Task<LoginResponseDto> CreateTokenAsync(AppUser appUser);
    }
}
