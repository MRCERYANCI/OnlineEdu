using FluentValidation;
using OnlineEdu.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.BusinessLayer.ValidationRules.CourseRules
{
    public class CourseValidator : AbstractValidator<Course>
    {
        public CourseValidator()
        {
            RuleFor(x => x.CourseName).NotEmpty().WithMessage("Bu Alan Zorunludur");
            RuleFor(x => x.CourseCategoryId).NotEmpty().WithMessage("Bu Alan Zorunludur");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Bu Alan Zorunludur");
        }
    }
}
