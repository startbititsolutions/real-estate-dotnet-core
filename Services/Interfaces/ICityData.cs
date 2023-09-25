 using Models.Database_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICityData
    {
        Task<IEnumerable<City>> GetAll();
        Task<City> GetById(int id);

       Task<City> GetByName(string name);
        Task<City> Insert(City obj);
        Task<City> Update(City obj, string id);
        Task Delete(string id);
        
        Task Save();
    }
}
