using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.EntityLayer.Entities
{
    public class BlogCategory
    {
        [Key]
        public int BlogCategoryId { get; set; }
        public string Name { get; set; }
        public bool status { get; set; }

        public ICollection<Blog> Blogs { get; set; }
    }
}
