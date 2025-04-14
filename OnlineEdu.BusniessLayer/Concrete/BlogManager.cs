using OnlineEdu.BusinessLayer.Abstract;
using OnlineEdu.BusniessLayer.Abstract;
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
    public class BlogManager : GenericManager<Blog>, IBlogService
    {
        private readonly IBlogDal _blogDal;
        public BlogManager(IGenericDal<Blog> genericDal, IBlogDal blogDal) : base(genericDal)
        {
            _blogDal = blogDal;
        }

        public async Task<int> TGetBlogCount()
        {
            return await _blogDal.GetBlogCount();
        }

        public async Task<List<Blog>> TGetLastFourBlogs()
        {
            return await _blogDal.GetLastFourBlogs();
        }

        public async Task<List<Blog>> TListBlogsWithCategories()
        {
            return await _blogDal.ListBlogsWithCategories();
        }

        public async Task<List<Blog>> TListBlogsWithCategoriesByUser(int appUserId)
        {
            return await _blogDal.ListBlogsWithCategoriesByUser(appUserId);
        }
    }
}
