
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<T> dbset;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            this.dbset = _context.Set<T>();
        }

        public async Task<T> AddData(T entity)
        {
            try
            {
                var result = await dbset.AddAsync(entity);
                return result.Entity;

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<T> DeleteData(int id)
        {
            try
            {

                var exist = await dbset.FindAsync(id);
                if (exist != null)
                {
                    var data = exist;
                    dbset.Remove(data as T);
                    return exist;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public async Task<T> GetByExpression(Expression<Func<T, bool>> expression)
        {
            try
            {
                var c = await dbset.Where(expression).FirstOrDefaultAsync();
                return c;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<IEnumerable<T>> GetByAllExpression(Expression<Func<T, bool>> expression)
        {
            try
            {
                var c = await dbset.Where(expression).ToListAsync();
                return c;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<T> EditData(T value)
        {
            try
            {
                if (value != null)
                {
                    await Task.Yield();
                    var pp = value;
                    dbset.Update(pp);
                    return value;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public async Task<T> GetMax<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            try
            {
                return await dbset.OrderByDescending(keySelector).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<T> GetMin<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            try
            {
                return await dbset.OrderBy(keySelector).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<IEnumerable<T>> GetData()
        {
            try
            {
                return await dbset.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        public async Task<T> GetDataById(int id)
        {
            try
            {
                return await dbset.FindAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<int> GetCount()
        {
            try
            {

              return await dbset.CountAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public void save()
        {
            _context.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetTopN(int i)
        {
            try
            {
                return await dbset.Take(i).ToListAsync();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}

