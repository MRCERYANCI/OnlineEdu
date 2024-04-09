using FluentValidation;
using OnlineEdu.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.BusniessLayer.ValidationRules.AboutRules
{
    public class AboutValidator : AbstractValidator<About>
    {
        public AboutValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Bu Alan Zorunludur*");
            RuleFor(x => x.Item1).NotEmpty().WithMessage("Bu Alan Zorunludur*");
            RuleFor(x => x.Item2).NotEmpty().WithMessage("Bu Alan Zorunludur*");
            RuleFor(x => x.Item3).NotEmpty().WithMessage("Bu Alan Zorunludur*");
            RuleFor(x => x.Item4).NotEmpty().WithMessage("Bu Alan Zorunludur*");
        }
    }
}
