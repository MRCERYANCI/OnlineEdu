
using System.Net;
using System.Net.Mail;

namespace OnlineEdu.PresentationLayer.Services.MailServices
{
    public class EMailSender : IEmailSender
    {
        public async Task SendConfirmedEmailAsync(string email, string Name_Surname, string userName, string urlLink)
        {
            string smtpServer = "mail.cokkececiyazilim.com";
            int smtpPort = 587; // 465 ise SSL gerekebilir
            string mailFrom = "webservice-noreply@cokkececiyazilim.com";
            string mailPassword = "199p*Xra8";

            string mailTo = email;
            string subject = $"Sayın {Name_Surname} Mail Onayı";
            string body = "<body style=\"font-family: 'Arial', sans-serif; background-color: #f9f9f9; margin: 0; padding: 0;\">";
            body += "<div style=\"max-width: 600px; margin: 40px auto; background: #ffffff; padding: 30px; border-radius: 8px; box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1); text-align: center;\">";
            body += "<div style=\"background: #007bff; color: white; padding: 20px; font-size: 24px; font-weight: bold; border-radius: 8px 8px 0 0;\">";
            body += "E-Posta Adresinizi Doğrulayın";
            body += "</div>";
            body += "<img src=\"https://bilgiakademisi.net.tr/Vitrin%20Temalar%C4%B1/onlineedu-master/assets/img/logo/logo.png\" alt=\"Bilgi Akademisi Logo\" style=\"width: 80px; margin: 20px 0;\">";
            body += "<div style=\"padding: 20px; font-size: 16px; color: #333; line-height: 1.6;\">";
            body += $"<p style=\"font-size: 18px; font-weight: bold; color: #007bff; margin-bottom: 10px;\">Merhaba, <span style=\"color:red;\">{userName}</span></p>";
            body += "<p>Hesabınızı güvenli hale getirmek için e-posta adresinizi doğrulamanız gerekiyor. Aşağıdaki butona tıklayarak işlemi tamamlayabilirsiniz. 📩</p>";
            body += $"<a href=\"{urlLink}\" style=\"display: inline-block; padding: 12px 30px; margin-top: 20px; font-size: 16px; color: white; background: #007bff; text-decoration: none; border-radius: 5px; font-weight: bold; transition: 0.3s;\">✅ E-Postamı Doğrula</a>";
            body += "<p>Eğer bu isteği siz göndermediyseniz, lütfen bizimle iletişime geçin. 📞</p>";
            body += "</div>";
            body += "<div style=\"margin-top: 30px; font-size: 14px; color: #666; border-top: 1px solid #ddd; padding-top: 15px;\">";
            body += $"&copy; {DateTime.Now.Year} Bilgi Akademisi | Güvenliğiniz Bizim İçin Önemli 🔒";
            body += "</div>";
            body += "</div>";

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(mailFrom, "BİLGİ AKADEMİSİ");

            mail.To.Add(mailTo);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true; // HTML formatında mail göndermek için

            mail.ReplyToList.Clear(); // Reply-To listesini temizler

            // SMTP istemcisi ayarları
            System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(mailFrom, mailPassword),
                EnableSsl = false // SSL kullanıyorsanız true yapın
            };

            // Mail gönderme
            smtpClient.Send(mail);
        }

        public async Task SendForgotPassword(string email, string urlLink, string userName, string nameSurname)
        {
            string smtpServer = "mail.cokkececiyazilim.com";
            int smtpPort = 587; // 465 ise SSL gerekebilir
            string mailFrom = "webservice-noreply@cokkececiyazilim.com";
            string mailPassword = "199p*Xra8";

            string mailTo = email;

            string subject = $"Sayın {nameSurname} Şifre Sıfırlama Talebiniz Doğrultusunda Gönderilmiştir";

            string body = "<body style=\"margin:0; padding:0; background-color:#f1f5f9; font-family:Segoe UI, sans-serif;\">";
            body += "<div style=\"max-width:600px; margin:40px auto; background-color:#ffffff; border-radius:12px; box-shadow:0 8px 24px rgba(0,0,0,0.08); overflow:hidden;\">";
            body += "<div style=\"background:linear-gradient(to right,#3b82f6,#06b6d4); color:white; padding:24px; text-align:center;\">";
            body += "<img src=\"https://i.imgur.com/M9sCzLm.png\" alt=\"Logo\" style=\"width:50px; margin-bottom:10px;\">";
            body += "<h1 style=\"margin:0; font-size:22px;\">Şifre Sıfırlama Talebi</h1>";
            body += "</div>";
            body += "<div style=\"padding:32px; color:#334155;\">";
            body += $"<h2 style=\"font-size:20px; margin-bottom:16px;\">Merhaba {userName}👋</h2>";
            body += "<p style=\"font-size:15px; line-height:1.6; margin-bottom:24px;\">";
            body += "Şifrenizi sıfırlamak için bir talepte bulundunuz. Aşağıdaki butona tıklayarak yeni bir şifre oluşturabilirsiniz.";
            body += "</p>";
            body += "<p style=\"text-align:center; margin-bottom:24px;\">";
            body += $"<a href=\"{urlLink}\" style=\"display:inline-block; background-color:#3b82f6; color:white; text-decoration:none; padding:12px 24px; border-radius:8px; font-weight:bold;\">Şifreyi Sıfırla</a>";
            body += "</p>";
            body += "<p style=\"font-size:15px; line-height:1.6;\">";
            body += "Bu isteği siz yapmadıysanız, lütfen bu e-postayı göz ardı ediniz. Şifreniz güvende kalacaktır.";
            body += "</p>";
            body += "</div>";
            body += "<div style=\"padding:24px; font-size:13px; color:#64748b; text-align:center; background-color:#f8fafc;\">";
            body += "Bu e-posta <strong>Bilgi Akademisi</strong> tarafından gönderilmiştir.";
            body += "<br>Destek için <a href=\"mailto:info@bilgiakademisi.net.tr\" style=\"color:#3b82f6; text-decoration:none;\">info@bilgiakademisi.net.tr</a> adresine ulaşabilirsiniz.";
            body += "</div>";
            body += "</div>";
            body += "</body>";


            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(mailFrom, "BİLGİ AKADEMİSİ");

            mail.To.Add(mailTo);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true; // HTML formatında mail göndermek için

            mail.ReplyToList.Clear(); // Reply-To listesini temizler

            // SMTP istemcisi ayarları
            System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(mailFrom, mailPassword),
                EnableSsl = false // SSL kullanıyorsanız true yapın
            };

            // Mail gönderme
            smtpClient.Send(mail);
        }
    }
}
