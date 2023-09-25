using Models.Database_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPropertyImageData
    {
         Task<IEnumerable<PropertyImage>> GetAll();
        Task<PropertyImage> GetById(int id);

        Task<IEnumerable<PropertyImage>> GetByPropertyId(int id);
        Task<PropertyImage> Insert(PropertyImage obj);
        Task<PropertyImage> Update(PropertyImage obj, string id);
        Task Delete(string id);
        Task Save();
    }
}
