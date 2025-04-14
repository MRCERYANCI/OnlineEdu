using OnlineEdu.DtoLayer.Dtos.CourseDtos;
using OnlineEdu.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DataAccessLayer.Abstract
{
    public interface ICourseDal : IGenericDal<Course>
    {
        Task<List<ResultCourseDto>> ListCourseWithCategories();
        Task<List<ResultCourseDto>> ListCourseWithCategories(Expression<Func<Course, bool>> filter = null);
        Task<List<Course>> ListCourseWithCategoriesAndTeacher(int appUserId);
        Task ShowOnHome(int courseId);
        Task DontShowOnHome(int courseId);
        Task<int> GetCourseCount();
        Task<List<Course>> GetCoursesByCategoryId(int courseId);
        Task<List<ResultCourseDto>> GetTopSixCourses();
        Task<List<ResultCourseDto>> GetAllCoursesForHomePage();

    }
}
