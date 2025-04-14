using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DtoLayer.Dtos.TeacherSocialMediaDtos
{
    public class ResultTeacherSocialMediaDto
    {
        public int TeacherSocialMediaId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public int TeacherId { get; set; }
    }
}
