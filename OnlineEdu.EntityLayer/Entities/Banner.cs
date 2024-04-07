using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.EntityLayer.Entities
{
    public class Banner
    {
        [Key]
        public int BannerId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
    }
}
