using OnlineEdu.DtoLayer.Dtos.BlogCategoryDtos;
using OnlineEdu.DtoLayer.Dtos.BlogCommentDtos;
using OnlineEdu.DtoLayer.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DtoLayer.Dtos.BlogDtos
{
    public class ResultBlogDto
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string SefUrl { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        public int BlogCategoryId { get; set; }
        public int AppUserId { get; set; }
        public ResultBlogCategoryDto BlogCategory { get; set; }
        public ResultUserDto AppUser { get; set; }
        public List<ResultBlogCommentDto> BlogComments { get; set; }
    }
}
