using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.EntityLayer.Entities
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int StudentId { get; set; }
        public AppUser Student { get; set; }
        public string Content { get; set; }
        public int Point { get; set; }
        public DateTime YorumTarihi { get; set; } = DateTime.Now;
    }
}
