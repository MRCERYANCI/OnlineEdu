using OnlineEdu.BusinessLayer.Abstract;
using OnlineEdu.BusniessLayer.Concrete;
using OnlineEdu.DataAccessLayer.Abstract;
using OnlineEdu.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.BusinessLayer.Concrete
{
    public class CourseCategoryManager : GenericManager<CourseCategory>, ICourseCategoryService
    {
        private readonly ICourseCategoryDal _courseCategoryDal;

        public CourseCategoryManager(IGenericDal<CourseCategory> _genericDal, ICourseCategoryDal courseCategoryDal) : base(_genericDal)
        {
            _courseCategoryDal = courseCategoryDal;
        }

        public async Task TDontShowOnHome(int courseCategoryId)
        {
            await _courseCategoryDal.DontShowOnHome(courseCategoryId);
        }

        public async Task<int> TGetCourseCategoryCount()
        {
            return await _courseCategoryDal.GetCourseCategoryCount();
        }

        public async Task TShowOnHome(int courseCategoryId)
        {
            await _courseCategoryDal.ShowOnHome(courseCategoryId);
        }
    }
}
