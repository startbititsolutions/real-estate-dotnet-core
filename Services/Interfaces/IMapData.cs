using Models.Database_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IMapData
    {
       Task<IEnumerable<Map>> GetAll();
        Task<Map> GetById(string id);

        Task<Map> GetByName(string name);
        Task<Map> Insert(Map obj);
        Task<Map> Update(Map obj, string id);
        Task Delete(string id);
        Task Save();
    }
}
