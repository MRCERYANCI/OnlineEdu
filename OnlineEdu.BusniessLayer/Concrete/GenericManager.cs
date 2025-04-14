using OnlineEdu.BusniessLayer.Abstract;
using OnlineEdu.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.BusniessLayer.Concrete
{
    public class GenericManager<T>(IGenericDal<T> _genericDal) : IGenericService<T> where T : class
    {
        public async Task<int> TCountAsync()
        {
            return await _genericDal.CountAsync();
        }

        public async Task TCreateAsync(T entity)
        {
            await _genericDal.CreateAsync(entity);
        }

        public async Task TDeleteAsync(int id)
        {
            await _genericDal.DeleteAsync(id);
        }

        public async Task<int> TFilteredCountAsync(Expression<Func<T, bool>> filter)
        {
            return await _genericDal.FilteredCountAsync(filter);
        }

        public async Task<List<T>> TGetAllAsync()
        {
            return await _genericDal.GetAllAsync();
        }

        public async Task<T> TGetByIdAsync(int id)
        {
            return await _genericDal.GetByIdAsync(id);
        }

        public async Task<T> TGetFilteredAsync(Expression<Func<T, bool>> filter)
        {
            return await _genericDal.GetFilteredAsync(filter);
        }

        public async Task<List<T>> TGetFilteredListAsync(Expression<Func<T, bool>> filter)
        {
            return await _genericDal.GetFilteredListAsync(filter);
        }

        public async Task<T> TGetFirstRecord(Expression<Func<T, object>> filter)
        {
            return await _genericDal.GetFirstRecord(filter);
        }

        public async Task TUpdateAsync(T entity)
        {
            await _genericDal.UpdateAsync(entity);
        }
    }
}
