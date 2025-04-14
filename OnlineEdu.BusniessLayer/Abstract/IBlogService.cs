using OnlineEdu.BusniessLayer.Abstract;
using OnlineEdu.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.BusinessLayer.Abstract
{
    public interface IBlogService : IGenericService<Blog>
    {
        Task<List<Blog>> TListBlogsWithCategories();
        Task<List<Blog>> TListBlogsWithCategoriesByUser(int appUserId);
        Task<List<Blog>> TGetLastFourBlogs();
        Task<int> TGetBlogCount();
    }
}
