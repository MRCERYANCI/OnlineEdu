using OnlineEdu.DtoLayer.Dtos.BlogDtos;
using OnlineEdu.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DataAccessLayer.Abstract
{
    public interface IBlogDal : IGenericDal<Blog>
    {
        Task<List<Blog>> ListBlogsWithCategories();
        Task<List<Blog>> ListBlogsWithCategoriesByUser(int appUserId);
        Task<List<Blog>> GetLastFourBlogs();
        Task<int> GetBlogCount();
        Task<Blog> GetBlogDetailsWithUser(string id);
        Task<List<Blog>> GetBlogsByCategory(int categoryId);
        Task<List<Blog>> SearchBlogPosts(string query);
    }
}
