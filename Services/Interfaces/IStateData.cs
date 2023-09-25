using Models.Database_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IStateData
    {
       Task<IEnumerable<State>> GetAll();
        Task<State> GetById(string id);

        Task<State> GetByName(string name);
        Task<State>Insert(State obj);
        Task<State>Update(State obj, string id);
        Task Delete(string id);
        Task Save();
    }
}
