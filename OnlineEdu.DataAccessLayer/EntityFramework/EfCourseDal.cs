using Microsoft.EntityFrameworkCore;
using OnlineEdu.DataAccessLayer.Abstract;
using OnlineEdu.DataAccessLayer.Concrete;
using OnlineEdu.DataAccessLayer.Repositories;
using OnlineEdu.DtoLayer.Dtos.CourseCategoryDtos;
using OnlineEdu.DtoLayer.Dtos.CourseDtos;
using OnlineEdu.EntityLayer.Entities;
using System.Linq.Expressions;


namespace OnlineEdu.DataAccessLayer.EntityFramework
{
    public class EfCourseDal : GenericRepository<Course>, ICourseDal
    {
        public EfCourseDal(OnlineEduContext onlineEduContext) : base(onlineEduContext)
        {
        }

        public async Task<List<ResultCourseDto>> ListCourseWithCategories()
        {
            var courses = await _onlineEduContext.Courses
           .Include(c => c.CourseCategory)
           .Include(c => c.Comments)
           .Select(c => new ResultCourseDto
           {
               CourseId = c.CourseId,
               CourseName = c.CourseName,
               ImageUrl = c.ImageUrl,
               CourseCategoryId = c.CourseCategoryId,
               Price = c.Price,
               ShowCase = c.ShowCase,
               Status = c.Status,

               CourseCategory = new ResultCourseCategoryDto
               {
                   CourseCategoryId = c.CourseCategory.CourseCategoryId,
                   CourseCategoryName = c.CourseCategory.CourseCategoryName
               },


               AverageScore = c.Comments.Any() ? Math.Round(c.Comments.Average(x => x.Point)) : 0,
               CommentCount = c.Comments.Sum(x => x.Point),
           })
           .ToListAsync();

            return courses;
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

        public async Task<List<Course>> ListCourseWithCategoriesAndTeacher(int appUserId)
        {
            return await _onlineEduContext.Courses.Where(x => x.AppUserId == appUserId).Include(x => x.CourseCategory).ToListAsync();
        }

        public async Task<int> GetCourseCount()
        {
            return await _onlineEduContext.Courses.Where(x => x.Status == true).CountAsync();
        }

        public async Task<List<Course>> GetCoursesByCategoryId(int courseId)
        {
            return await _onlineEduContext.Courses.Where(x => x.CourseCategoryId == courseId).OrderByDescending(x => x.CourseName).Include(x => x.CourseCategory).ToListAsync();
        }

        public async Task<List<ResultCourseDto>> ListCourseWithCategories(Expression<Func<Course, bool>> filter = null)
        {
            IQueryable<Course> query = _onlineEduContext.Courses.Include(x => x.CourseCategory).Include(z => z.AppUser).ThenInclude(c => c.TeacherSocialMedias).AsQueryable(); //Liste İçinde Sorgu Yapabilmek için

            if (filter is not null)
            {
                query = query.Where(filter);
            }

            var values = query.Select(c => new ResultCourseDto
            {
                CourseId = c.CourseId,
                CourseName = c.CourseName,
                ImageUrl = c.ImageUrl,
                CourseCategoryId = c.CourseCategoryId,
                Price = c.Price,
                ShowCase = c.ShowCase,
                Status = c.Status,

                CourseCategory = new ResultCourseCategoryDto
                {
                    CourseCategoryId = c.CourseCategory.CourseCategoryId,
                    CourseCategoryName = c.CourseCategory.CourseCategoryName
                },


                AverageScore = c.Comments.Any() ? Math.Round(c.Comments.Average(x => x.Point)) : 0,
                CommentCount = c.Comments.Sum(x => x.Point),
            });

            return await values.ToListAsync();
        }

        public async Task<List<ResultCourseDto>> GetTopSixCourses()
        {
            var courses = await _onlineEduContext.Courses
           .Include(c => c.CourseCategory)
           .Include(c => c.Comments)
           .Select(c => new ResultCourseDto
           {
               CourseId = c.CourseId,
               CourseName = c.CourseName,
               ImageUrl = c.ImageUrl,
               CourseCategoryId = c.CourseCategoryId,
               Price = c.Price,
               ShowCase = c.ShowCase,
               Status = c.Status,

               CourseCategory = new ResultCourseCategoryDto
               {
                   CourseCategoryId = c.CourseCategory.CourseCategoryId,
                   CourseCategoryName = c.CourseCategory.CourseCategoryName
               },


               AverageScore = c.Comments.Any() ? Math.Round(c.Comments.Average(x => x.Point)) : 0,
               CommentCount = c.Comments.Sum(x => x.Point),
           }).Where(x => x.Status == true && x.ShowCase == true).Take(9)
           .ToListAsync();

            return courses;
        }

        public async Task<List<ResultCourseDto>> GetAllCoursesForHomePage()
        {
            var courses = await _onlineEduContext.Courses
         .Include(c => c.CourseCategory)
         .Include(c => c.Comments)
         .Select(c => new ResultCourseDto
         {
             CourseId = c.CourseId,
             CourseName = c.CourseName,
             ImageUrl = c.ImageUrl,
             CourseCategoryId = c.CourseCategoryId,
             Price = c.Price,
             ShowCase = c.ShowCase,
             Status = c.Status,

             CourseCategory = new ResultCourseCategoryDto
             {
                 CourseCategoryId = c.CourseCategory.CourseCategoryId,
                 CourseCategoryName = c.CourseCategory.CourseCategoryName
             },


             AverageScore = c.Comments.Any() ? Math.Round(c.Comments.Average(x => x.Point)) : 0,
             CommentCount = c.Comments.Sum(x => x.Point),
         }).Where(x => x.Status == true)
         .ToListAsync();

            return courses;
        }
    }
}
