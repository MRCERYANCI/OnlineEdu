using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DtoLayer.Dtos.CourseDtos
{
    public class CreateCourseDto
    {
        public string CourseName { get; set; }
        public string ImageUrl { get; set; }
        public int CourseCategoryId { get; set; }
        public decimal Price { get; set; }
        public bool ShowCase { get; set; } = false;
        public int AppUserId { get; set; }
        public bool Status { get; set; } = true;
    }
}
