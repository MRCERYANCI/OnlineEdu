﻿using FluentValidation;
using OnlineEdu.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.BusinessLayer.ValidationRules.BlogCategoryRules
{
    public class BlogCategoryValidator : AbstractValidator<BlogCategory>
    {
        public BlogCategoryValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Bu Alan Zorunludur");
        }
    }
}
