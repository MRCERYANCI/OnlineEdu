using FluentValidation;
using OnlineEdu.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.BusinessLayer.ValidationRules.CourseCategoryRules
{
    public class CourseCategoryValidator : AbstractValidator<CourseCategory>
    {

        public CourseCategoryValidator()
        {
            RuleFor(x => x.CourseCategoryName).NotEmpty().WithMessage("Bu Alan Zorunludur");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Bu Alan Zorunludur");
            RuleFor(x => x.Icon).NotEmpty().WithMessage("Bu Alan Zorunludur");
        }
    }
}
