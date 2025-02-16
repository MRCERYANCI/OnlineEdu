using OnlineEdu.DataAccessLayer.Abstract;
using OnlineEdu.DataAccessLayer.Concrete;
using OnlineEdu.DataAccessLayer.Repositories;
using OnlineEdu.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DataAccessLayer.EntityFramework
{
    public class EfCourseCategoryDal : GenericRepository<CourseCategory>, ICourseCategoryDal
    {
        public EfCourseCategoryDal(OnlineEduContext onlineEduContext) : base(onlineEduContext)
        {
            
        }

        public async Task DontShowOnHome(int courseCategoryId)
        {
            var value = await _onlineEduContext.CourseCategories.FindAsync(courseCategoryId);
            if (value.ShowCase)
            {
                value.ShowCase = false;
                await _onlineEduContext.SaveChangesAsync();
            }
        }

        public async Task ShowOnHome(int courseCategoryId)
        {
            var value = await _onlineEduContext.CourseCategories.FindAsync(courseCategoryId);
            if (!value.ShowCase)
            {
                value.ShowCase = true;
                await _onlineEduContext.SaveChangesAsync();
            }
        }
    }
}
