using OnlineEdu.DtoLayer.Dtos.TeacherSocialMediaDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DtoLayer.Dtos.UserDtos
{
    public class ResultUserDto
    {
        public int Id { get; set; }
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public string ProfileImageUrl { get; set; }
        public List<ResultTeacherSocialMediaDto> TeacherSocialMedias { get; set; }
    }
}
