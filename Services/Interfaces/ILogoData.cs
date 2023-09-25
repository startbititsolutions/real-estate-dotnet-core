using Models.Database_Models;
using Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ILogoData
    {
        Task<IEnumerable<Logo>> GetAll();
        Task<Logo> GetById(string id);


        Task<Logo> Insert(Logo obj);
        Task<Logo> Update(Logo obj, string id);
        Task Delete(string id);
        Task Save();
    }
}
