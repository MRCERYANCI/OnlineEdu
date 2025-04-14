using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.EntityLayer.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public string? ProfileImageUrl { get; set; }

        public List<Course> Courses { get; set; }
        public List<CourseRegister> CourseRegisters { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<TeacherSocialMedia> TeacherSocialMedias { get; set; }
    }
}
