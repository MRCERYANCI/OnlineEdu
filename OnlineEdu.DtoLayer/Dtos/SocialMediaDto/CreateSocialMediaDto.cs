using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DtoLayer.Dtos.SocialMediaDto
{
    public class CreateSocialMediaDto
    {
        public string Icon { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get => DateTime.Now; }
        public bool IsActive { get; set; }
    }
}
