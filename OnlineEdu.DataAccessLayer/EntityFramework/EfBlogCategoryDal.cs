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
    public class EfBlogCategoryDal : GenericRepository<BlogCategory>, IBlogCategoryDal
    {
        public EfBlogCategoryDal(OnlineEduContext onlineEduContext) : base(onlineEduContext)
        {
            
        }

        public async Task<List<BlogCategory>> GetCategoriesWithBlogs()
        {
            return await _onlineEduContext.BlogCategories.Include(blog => blog.Blogs).ToListAsync();
        }
    }
}
