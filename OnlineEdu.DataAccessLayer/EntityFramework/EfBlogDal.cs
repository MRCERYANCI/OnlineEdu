using Microsoft.EntityFrameworkCore;
using OnlineEdu.DataAccessLayer.Abstract;
using OnlineEdu.DataAccessLayer.Concrete;
using OnlineEdu.DataAccessLayer.Repositories;
using OnlineEdu.DtoLayer.Dtos.BlogDtos;
using OnlineEdu.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DataAccessLayer.EntityFramework
{
    public class EfBlogDal : GenericRepository<Blog>, IBlogDal
    {
        private readonly OnlineEduContext _onlineEduContext;
        public EfBlogDal(OnlineEduContext onlineEduContext) : base(onlineEduContext)
        {
            _onlineEduContext = onlineEduContext;
        }

        public async Task<int> GetBlogCount()
        {
            return await _onlineEduContext.Blogs.Where(x => x.Status == true).CountAsync();
        }

        public async Task<Blog> GetBlogDetailsWithUser(string id)
        {
            return await _onlineEduContext.Blogs.Include(blogComments => blogComments.BlogComments).Include(blogCategory => blogCategory.BlogCategory).Include(user => user.AppUser).ThenInclude(teacherSocialMedias => teacherSocialMedias.TeacherSocialMedias).Where(x => x.SefUrl == id && x.Status == true).FirstOrDefaultAsync();
        }

        public async Task<List<Blog>> GetBlogsByCategory(int categoryId)
        {
            return await _onlineEduContext.Blogs.Include(blogCategory => blogCategory.BlogCategory).Include(blogComments => blogComments.BlogComments).Include(user => user.AppUser).Where(x => x.BlogCategoryId == categoryId && x.Status == true).ToListAsync();
        }

        public async Task<List<Blog>> GetLastFourBlogs()
        {
            return await _onlineEduContext.Blogs.OrderByDescending(y => y.CreatedDate).Where(z => z.Status == true).Take(4).Include(x => x.BlogCategory).ToListAsync();
        }

        public async Task<List<Blog>> ListBlogsWithCategories()
        {
            return await _onlineEduContext.Blogs.Include(x => x.BlogCategory).Include(y => y.BlogComments).ToListAsync();
        }

        public async Task<List<Blog>> ListBlogsWithCategoriesByUser(int appUserId)
        {
            return await _onlineEduContext.Blogs.Where(x => x.AppUserId == appUserId).Include(x => x.BlogCategory).ToListAsync();
        }

        public async Task<List<Blog>> SearchBlogPosts(string query)
        {
            if (_onlineEduContext.Blogs == null)
                throw new Exception("Blogs tablosu yüklenemedi!");

            return await _onlineEduContext.Blogs.Where(x => x.Title.Contains(query) && x.Status == true).Include(blogCategory => blogCategory.BlogCategory).Include(appuser => appuser.AppUser).Include(blocComment => blocComment.BlogComments).ToListAsync();
        }
    }
}
