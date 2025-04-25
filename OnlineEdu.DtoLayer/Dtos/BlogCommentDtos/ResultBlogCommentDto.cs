using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DtoLayer.Dtos.BlogCommentDtos
{
    public class ResultBlogCommentDto
    {
        public int BlogCommentId { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }
        public int BlogId { get; set; }
    }
}
