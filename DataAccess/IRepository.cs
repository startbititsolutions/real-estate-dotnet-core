using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetData();
        Task<T> GetDataById(int id);
        Task<T> EditData(T value);
        Task<T> DeleteData(int id);
        Task<T> AddData(T entity);
        Task<T> GetByExpression(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetByAllExpression(Expression<Func<T, bool>> expression);
        Task<T> GetMax<TKey>(Expression<Func<T, TKey>> keySelector);
        Task<T> GetMin<TKey>(Expression<Func<T, TKey>> keySelector);
        Task<int> GetCount();
        Task<IEnumerable<T>> GetTopN(int i);
        void save();
    }
}
