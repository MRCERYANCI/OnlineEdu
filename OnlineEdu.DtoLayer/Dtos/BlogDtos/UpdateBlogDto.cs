using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DtoLayer.Dtos.BlogDtos
{
    public class UpdateBlogDto
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public string SefUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        public int BlogCategoryId { get; set; }
        public int AppUserId { get; set; }
    }
}
