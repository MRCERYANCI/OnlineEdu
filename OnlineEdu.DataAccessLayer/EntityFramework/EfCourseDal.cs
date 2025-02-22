using Microsoft.EntityFrameworkCore;
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
    public class EfCourseDal : GenericRepository<Course>, ICourseDal
    {
        public EfCourseDal(OnlineEduContext onlineEduContext) : base(onlineEduContext)
        {
            
        }

        public async Task<List<Course>> ListCourseWithCategories()
        {
            return await _onlineEduContext.Courses.Include(x => x.CourseCategory).ToListAsync();
        }

        public async Task DontShowOnHome(int courseId)
        {
            var value = await _onlineEduContext.Courses.FindAsync(courseId);
            if (value.ShowCase)
            {
                value.ShowCase = false;
                await _onlineEduContext.SaveChangesAsync();
            }
        }

        public async Task ShowOnHome(int courseId)
        {
            var value = await _onlineEduContext.Courses.FindAsync(courseId);
            if (!value.ShowCase)
            {
                value.ShowCase = true;
                await _onlineEduContext.SaveChangesAsync();
            }
        }
    }
}
