using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DtoLayer.Dtos.CourseCategoryDtos
{
    public class CreateCourseCategoryDto
    {
        public string CourseCategoryName { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public bool ShowCase { get; set; }
    }
}
