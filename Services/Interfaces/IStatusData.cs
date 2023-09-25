using Models.Database_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IStatusData
    {
        Task<IEnumerable<Status>> GetAll();
        Task<Status> GetById(int id);
        Task<Status> GetByPropertyId(int id);
        Task<Status> GetByName(string name);
        Task<Status> Insert(Status obj);
        Task<Status> Update(Status obj, string id);
        Task Delete(string id);
        Task Save();
    }
}
