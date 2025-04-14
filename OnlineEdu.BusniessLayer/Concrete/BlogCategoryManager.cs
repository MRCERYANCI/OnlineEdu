using OnlineEdu.BusinessLayer.Abstract;
using OnlineEdu.BusniessLayer.Concrete;
using OnlineEdu.DataAccessLayer.Abstract;
using OnlineEdu.EntityLayer.Entities;

namespace OnlineEdu.BusinessLayer.Concrete
{
    public class BlogCategoryManager : GenericManager<BlogCategory>, IBlogCategoryService
    {
        private readonly IBlogCategoryDal _blogCategoryDal;

        public BlogCategoryManager(IBlogCategoryDal blogCategoryDal, IGenericDal<BlogCategory> _genericDal) : base(_genericDal)
        {
            _blogCategoryDal = blogCategoryDal;
        }

        public async Task<List<BlogCategory>> TGetCategoriesWithBlogs()
        {
            return await _blogCategoryDal.GetCategoriesWithBlogs();
        }
    }
}
