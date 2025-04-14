using OnlineEdu.BusniessLayer.Abstract;
using OnlineEdu.EntityLayer.Entities;


namespace OnlineEdu.BusinessLayer.Abstract
{
    public interface IBlogCategoryService : IGenericService<BlogCategory>
    {
        Task<List<BlogCategory>> TGetCategoriesWithBlogs();
    }
}
