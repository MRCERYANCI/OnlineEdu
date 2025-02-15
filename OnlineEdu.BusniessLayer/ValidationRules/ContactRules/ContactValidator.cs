using FluentValidation;
using OnlineEdu.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.BusinessLayer.ValidationRules.ContactRules
{
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(x => x.MapUrl).NotEmpty().WithMessage("Bu Alan Zorunludur");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Bu Alan Zorunludur");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Bu Alan Zorunludur");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Bu Alan Zorunludur");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Lütfen Geçerli Bir Mail Adresi Giriniz");
        }
    }
}
