using Microsoft.EntityFrameworkCore;
using OnlineEdu.DataAccessLayer.Abstract;
using OnlineEdu.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DataAccessLayer.Repositories
{
    public class GenericRepository<T>(OnlineEduContext _onlineEduContext) : IGenericDal<T> where T : class
    {
        public DbSet<T> Table { get => _onlineEduContext.Set<T>();}

        public async Task<int> CountAsync()
        {
            return await Table.CountAsync();
        }

        public async Task CreateAsync(T entity)
        {
            await Table.AddRangeAsync(entity);
            await _onlineEduContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var values =  await GetByIdAsync(id);
            if(values != null)
            {
                Table.Remove(values);
                await _onlineEduContext.SaveChangesAsync();
            }
        }

        public async Task<int> FilteredCountAsync(Expression<Func<T, bool>> filter)
        {
            return await Table.Where(filter).CountAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
           return await Table.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await Table.FindAsync(id);
        }

        public async Task<T> GetFilteredAsync(Expression<Func<T, bool>> filter)
        {
            return await Table.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetFilteredListAsync(Expression<Func<T, bool>> filter)
        {
            return await Table.Where(filter).ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            Table.Update(entity);
            await _onlineEduContext.SaveChangesAsync();
        }
    }
}
