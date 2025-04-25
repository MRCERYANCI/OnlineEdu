using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DtoLayer.Dtos.CourseVideoDtos
{
    public class CreateCourseVideoDto
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int VideoNumber { get; set; }
        public List<IFormFile> Thumbnails { get; set; }
        public List<IFormFile> Video { get; set; }
    }
}
