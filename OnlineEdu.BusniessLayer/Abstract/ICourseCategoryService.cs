﻿using OnlineEdu.BusniessLayer.Abstract;
using OnlineEdu.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.BusinessLayer.Abstract
{
    public interface ICourseCategoryService : IGenericService<CourseCategory>
    {
        Task TShowOnHome(int courseCategoryId);
        Task TDontShowOnHome(int courseCategoryId);
        Task<int> TGetCourseCategoryCount();
    }
}
