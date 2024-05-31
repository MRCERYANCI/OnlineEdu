using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DtoLayer.Dtos.BannerDtos
{
    public class CreateBannerDto
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile Image { get; set; }
    }
}
