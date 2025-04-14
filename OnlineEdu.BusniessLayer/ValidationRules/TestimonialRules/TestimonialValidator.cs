using FluentValidation;
using OnlineEdu.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.BusinessLayer.ValidationRules.TestimonialRules
{
    public class TestimonialValidator : AbstractValidator<Testimonial>
    {
        public TestimonialValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Bu Alan Zorunludur");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Bu Alan Zorunludur");
            RuleFor(x => x.Comment).NotEmpty().WithMessage("Bu Alan Zorunludur");
            RuleFor(x => x.Star).NotEmpty().WithMessage("Bu Alan Zorunludur");
        }
    }
}
