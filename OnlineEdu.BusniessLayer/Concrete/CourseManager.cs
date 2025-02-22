using OnlineEdu.BusinessLayer.Abstract;
using OnlineEdu.BusniessLayer.Concrete;
using OnlineEdu.DataAccessLayer.Abstract;
using OnlineEdu.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<Course>> TListCourseWithCategories()
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
    }
}
