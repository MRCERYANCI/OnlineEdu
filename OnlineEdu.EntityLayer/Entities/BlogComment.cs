using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.EntityLayer.Entities
{
    public class BlogComment
    {
        [Key]
        public int BlogCommentId { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
