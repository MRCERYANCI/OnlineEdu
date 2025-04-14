using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DtoLayer.Dtos.RoleDtos
{
    public class CreateRoleDto
    {
        public string Name { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
