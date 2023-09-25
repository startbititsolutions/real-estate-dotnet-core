using Models.Database_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IFeatureData
    {
        Task<IEnumerable<Features>> GetAll();
        Task<Features> GetByPropertyId(int id);
        Task<Features> GetById(int id);
        Task<Features> GetByName(string name);
        Task<Features> Insert(Features obj);
        Task<Features> Update(Features obj, string id);
        Task Delete(string id);
        Task Save();
    }
}
