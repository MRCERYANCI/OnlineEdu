using OnlineEdu.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DataAccessLayer.Abstract
{
    public interface ICourseDal : IGenericDal<Course>
    {
        Task<List<Course>> ListCourseWithCategories();
        Task ShowOnHome(int courseId);
        Task DontShowOnHome(int courseId);
    }
}
