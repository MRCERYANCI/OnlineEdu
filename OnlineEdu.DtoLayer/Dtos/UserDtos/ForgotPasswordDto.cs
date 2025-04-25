using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DtoLayer.Dtos.UserDtos
{
    public class ForgotPasswordDto
    {
        [Required(ErrorMessage = "Bu Alan Zorunludur")]
        [EmailAddress(ErrorMessage = "Lütfen Geçerli Bir Mail Adresi Giriniz")]
        public string Email { get; set; }
    }
}
