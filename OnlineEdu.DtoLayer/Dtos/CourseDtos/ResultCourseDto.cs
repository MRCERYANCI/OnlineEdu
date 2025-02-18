using OnlineEdu.DtoLayer.Dtos.CourseCategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DtoLayer.Dtos.CourseDtos
{
    public class ResultCourseDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string ImageUrl { get; set; }
        public int CourseCategoryId { get; set; }
        public decimal Price { get; set; }
        public bool ShowCase { get; set; }
        public bool Status { get; set; }
        public ResultCourseCategoryDto CourseCategory { get; set; }
    }
}
