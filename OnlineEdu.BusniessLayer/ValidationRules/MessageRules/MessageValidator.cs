using FluentValidation;
using OnlineEdu.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.BusinessLayer.ValidationRules.MessageRules
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Bu Alan Zorunludur");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Bu Alan Zorunludur");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Lütfen Geeçerli Bir Mail Adresi Giriniz");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Bu Alan Zorunludur");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Bu Alan Zorunludur");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Bu Alan Zorunludur");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Bu Alan Zorunludur");
        }
    }
}
