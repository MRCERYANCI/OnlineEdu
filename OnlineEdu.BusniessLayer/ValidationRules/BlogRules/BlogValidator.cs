using FluentValidation;
using OnlineEdu.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.BusinessLayer.ValidationRules.BlogRules
{
    public class BlogValidator : AbstractValidator<Blog>
    {
        public BlogValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Bu Alan Zorunludur");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Bu Alan Zorunludur");
            RuleFor(x => x.CreatedDate).NotEmpty().WithMessage("Bu Alan Zorunludur");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Bu Alan Zorunludur");
            RuleFor(x => x.BlogCategoryId).NotEmpty().WithMessage("Bu Alan Zorunludur");
        }
    }
}
