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
    public class EfBlogDal : GenericRepository<Blog>, IBlogDal
    {
        private readonly OnlineEduContext _onlineEduContext;
        public EfBlogDal(OnlineEduContext onlineEduContext) : base(onlineEduContext)
        {
            _onlineEduContext = onlineEduContext;
        }

        public async Task<List<Blog>> ListBlogsWithCategories()
        {
           return await _onlineEduContext.Blogs.Include(x=>x.BlogCategory).ToListAsync();
        }
    }
}
