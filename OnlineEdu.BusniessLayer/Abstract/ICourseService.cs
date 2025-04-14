using OnlineEdu.BusniessLayer.Abstract;
using OnlineEdu.DtoLayer.Dtos.CourseDtos;
using OnlineEdu.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.BusinessLayer.Abstract
{
    public interface ICourseService : IGenericService<Course>
    {
        Task<List<ResultCourseDto>> TListCourseWithCategories();
        Task<List<Course>> TListCourseWithCategoriesAndTeacher(int appUserId);
        Task TShowOnHome(int courseId);
        Task TDontShowOnHome(int courseId);
        Task<int> TGetCourseCount();
        Task<List<Course>> TGetCoursesByCategoryId(int courseId);
        Task<List<ResultCourseDto>> TListCourseWithCategories(Expression<Func<Course, bool>> filter = null);
        Task<List<ResultCourseDto>> TGetTopSixCourses();
        Task<List<ResultCourseDto>> TGetAllCoursesForHomePage();
    }
}
