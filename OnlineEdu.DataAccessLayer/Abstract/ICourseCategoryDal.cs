using OnlineEdu.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DataAccessLayer.Abstract
{
    public interface ICourseCategoryDal : IGenericDal<CourseCategory>
    {
        Task ShowOnHome(int courseCategoryId);
        Task DontShowOnHome(int courseCategoryId);
        Task<int> GetCourseCategoryCount();
    }
}
