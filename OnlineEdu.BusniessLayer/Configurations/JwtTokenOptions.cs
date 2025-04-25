using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.BusinessLayer.Configurations
{
    public class JwtTokenOptions
    {
        public string Issuer { get; set; } //Sağlayıcı webapi.bilgiakademisi.net.tr
        public string Audience { get; set; } //Dinleyici www.bilgiakademisi.net.tr
        public string Key { get; set; }
        public int ExpireInMinutes { get; set; }
    }
}
