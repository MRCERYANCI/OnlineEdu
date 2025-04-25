using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DtoLayer.Dtos.CourseVideoDtos
{
    public class ResultCourseVideoDto
    {
        public int CourseVideoId { get; set; }
        public string Title { get; set; }
        public int CourseId { get; set; }
        public int VideoNumber { get; set; }
        public string Thumbnails { get; set; }
        public string Video { get; set; }
    }
}
