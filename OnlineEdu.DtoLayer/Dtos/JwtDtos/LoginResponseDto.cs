using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DtoLayer.Dtos.JwtDtos
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
