using Microsoft.EntityFrameworkCore;
using PostApi.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PostApi.Data.Repositorio
{
    public class RepositorioGenerico<T> : IRepositorioGenerico<T> where T : class
    {
        private AppDbContext _dbContext;

        public RepositorioGenerico(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> Add(T t)
        {
            _dbContext.Entry(t).State = EntityState.Added;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return t;
        }

        public async Task<bool> Delete(int? id)
        {
            var t = await _dbContext.Set<T>().FindAsync(id);

            _dbContext.Entry(t).State = EntityState.Deleted;
            try
            {
                return await _dbContext.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<T> Find(int? id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetByCondition(Expression<Func<T, bool>> expression, int skip, int take)
        {
            return await _dbContext.Set<T>().Where(expression).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<List<T>> Get()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<bool> Update(T t)
        {
            _dbContext.Set<T>().Attach(t);
            _dbContext.Entry(t).State = EntityState.Modified;
            try
            {
                return await _dbContext.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteByCondition(Expression<Func<T, bool>> expression)
        {
            var t = await _dbContext.Set<T>().Where(expression).FirstOrDefaultAsync();
            _dbContext.Entry(t).State = EntityState.Deleted;
            try
            {
                return await _dbContext.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
