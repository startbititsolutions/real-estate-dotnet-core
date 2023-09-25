using Models.Database_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBlogData
    {
        Task<IEnumerable<Blog>> GetAll();
        Task<Blog> GetById(int id);


        Task<Blog> Insert(Blog obj);
        Task<Blog> Update(Blog obj, int id);
        Task<Blog> Delete(int id);
        Task Save();
    }
}

