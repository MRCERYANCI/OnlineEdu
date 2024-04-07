using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OnlineEdu.DataAccessLayer.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<int> CountAsync();
        Task<int> FilteredCountAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> GetFilteredListAsync(Expression<Func<T, bool>> filter);
        Task<T> GetFilteredAsync(Expression<Func<T, bool>> filter);
    }
}
