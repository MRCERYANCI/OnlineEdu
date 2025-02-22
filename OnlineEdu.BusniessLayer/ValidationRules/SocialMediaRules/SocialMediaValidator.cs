using FluentValidation;
using OnlineEdu.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.BusinessLayer.ValidationRules.SocialMediaRules
{
    public class SocialMediaValidator : AbstractValidator<SocialMedia>
    {
        public SocialMediaValidator()
        {
            RuleFor(x => x.Icon).NotEmpty().WithMessage("Bu Alan Zorunludur");
            RuleFor(x => x.Url).NotEmpty().WithMessage("Bu Alan Zorunludur");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Bu Alan Zorunludur");
        }
    }
}
