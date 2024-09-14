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
    }
}
