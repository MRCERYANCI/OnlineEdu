using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.BusniessLayer.Abstract
{
    public interface IGenericService<T> where T : class
    {
        Task TCreateAsync(T entity);
        Task TUpdateAsync(T entity);
        Task TDeleteAsync(int id);
        Task<List<T>> TGetAllAsync();
        Task<T> TGetByIdAsync(int id);
        Task<int> TCountAsync();
        Task<int> TFilteredCountAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> TGetFilteredListAsync(Expression<Func<T, bool>> filter);
        Task<T> TGetFilteredAsync(Expression<Func<T, bool>> filter);
    }
}
