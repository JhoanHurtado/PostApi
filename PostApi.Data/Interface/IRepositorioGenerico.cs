using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PostApi.Data.Interface
{
    public interface IRepositorioGenerico<T> where T : class
    {
        Task<List<T>> Get();
        Task<T> Find(int? id);
        Task<List<T>> GetByCondition(Expression<Func<T, bool>> expression,int skip, int take);
        Task<bool> DeleteByCondition(Expression<Func<T, bool>> expression);
        Task<T> Add(T t);
        Task<bool> Update(T t);
        Task<bool> Delete(int? id);
    }
}
