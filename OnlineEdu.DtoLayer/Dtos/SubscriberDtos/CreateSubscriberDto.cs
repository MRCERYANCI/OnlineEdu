using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DtoLayer.Dtos.SubscriberDtos
{
    public class CreateSubscriberDto
    {
        public string Email { get; set; }
        public DateTime CreatedDate { get => DateTime.Now; }
        public bool IsActive { get => false; }
    }
}
