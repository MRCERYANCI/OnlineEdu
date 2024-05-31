using FluentValidation;
using OnlineEdu.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.BusinessLayer.ValidationRules.BannerRules
{
    public class BannerValidator : AbstractValidator<Banner>
    {
        public BannerValidator()
        {
            RuleFor(x=>x.Title).NotEmpty().WithMessage("Bu Alan Zorunludur");
            RuleFor(x=>x.Title).MinimumLength(5).WithMessage("Minimum 5 Karakter Veri Girişi Yapılabilir");
        }
    }
}
