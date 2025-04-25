using FluentValidation;
using OnlineEdu.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.BusinessLayer.ValidationRules.SubscribeRules
{
    public class SubscribeValidator : AbstractValidator<Subscriber>
    {
        public SubscribeValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Bu Alan Zorunludur");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Doğru Formatta Mail Adresi Giriniz");
        }
    }
}
