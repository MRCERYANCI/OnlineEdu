using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DtoLayer.Dtos.BlogCategoryDtos
{
    public class ResultBlogCategoryWithCountDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Count { get; set; }
    }
}
