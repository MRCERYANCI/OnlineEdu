using OnlineEdu.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DataAccessLayer.Abstract
{
    public interface IBlogCategoryDal : IGenericDal<BlogCategory>
    {
        Task<List<BlogCategory>> GetCategoriesWithBlogs();
    }
}
