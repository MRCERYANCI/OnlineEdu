using OnlineEdu.BusinessLayer.Abstract;
using OnlineEdu.BusniessLayer.Concrete;
using OnlineEdu.DataAccessLayer.Abstract;
using OnlineEdu.DtoLayer.Dtos.CourseDtos;
using OnlineEdu.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.BusinessLayer.Concrete
{
    public class CourseManager : GenericManager<Course>, ICourseService
    {
        private readonly ICourseDal _courseDal;

        public CourseManager(IGenericDal<Course> _genericDal, ICourseDal courseDal) : base(_genericDal)
        {
            _courseDal = courseDal;
        }

        public async Task<List<ResultCourseDto>> TListCourseWithCategories()
        {
            return await _courseDal.ListCourseWithCategories();

        }

        public async Task TDontShowOnHome(int courseId)
        {
            await _courseDal.DontShowOnHome(courseId);
        }

        public async Task TShowOnHome(int courseId)
        {
            await _courseDal.ShowOnHome(courseId);
        }

        public async Task<List<Course>> TListCourseWithCategoriesAndTeacher(int appUserId)
        {
            return await _courseDal.ListCourseWithCategoriesAndTeacher(appUserId);
        }

        public async Task<int> TGetCourseCount()
        {
            return await _courseDal.CountAsync();
        }

        public async Task<List<Course>> TGetCoursesByCategoryId(int courseId)
        {
            return await _courseDal.GetCoursesByCategoryId(courseId);
        }

        public async Task<List<ResultCourseDto>> TListCourseWithCategories(Expression<Func<Course, bool>> filter = null)
        {
            return await _courseDal.ListCourseWithCategories(filter);
        }

        public async Task<List<ResultCourseDto>> TGetTopSixCourses()
        {
            return await _courseDal.GetTopSixCourses();
        }

        public async Task<List<ResultCourseDto>> TGetAllCoursesForHomePage()
        {
            return await _courseDal.GetAllCoursesForHomePage();
        }
    }
}
