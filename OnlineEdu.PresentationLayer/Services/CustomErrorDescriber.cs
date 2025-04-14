using Microsoft.AspNetCore.Identity;

namespace OnlineEdu.PresentationLayer.Services
{
    public class CustomErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError
            {
                Description = "Şifre En Az Bir Rakam İçermelidir[0-9]"
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError
            {
                Description = "Şifre En Az Bir Küçük Harf İçermelidir[a-z]"
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError
            {
                Description = "Şifre En Az Bir Özel Karakter İçermelidir(.,-@_+*)"
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError
            {
                Description = $"Şifre En Az {length} Karakter İçermelidir"
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Description = "Şifre En Az Bir Büyük Harf İçermelidir[A-Z]"
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Description = $"{userName} Kullanıcı Adı Saten Kayıtlıdır Başka Bir Kullanıcı Adı Seçin"
            };
        }
    }
}
