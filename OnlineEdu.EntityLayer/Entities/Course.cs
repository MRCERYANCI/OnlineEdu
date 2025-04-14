using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.EntityLayer.Entities
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string ImageUrl { get; set; }
        public int CourseCategoryId { get; set; }
        public CourseCategory CourseCategory { get; set; }
        public decimal Price { get; set; }
        public bool ShowCase { get; set; }
        public bool Status { get; set; }

        public int? AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public List<CourseRegister> CourseRegisters { get; set; }
        public List<Comment> Comments { get; set; }

    }
}
