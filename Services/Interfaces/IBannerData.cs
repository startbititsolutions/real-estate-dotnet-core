using Models.Database_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBannerData
    {
        Task<IEnumerable<Banner>> GetAll();
        Task<Banner> GetById(int id);


        Task<Banner>Insert(Banner obj);
        Task<Banner> Update(Banner obj, int id);
        Task<Banner> Delete(int id);
        Task Save();
    }
}
