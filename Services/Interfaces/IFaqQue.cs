using Models.Database_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IFaqQue
    {
        Task<IEnumerable<FaqQue>> GetAll();
        Task<FaqQue> GetById(int id);
      
        Task<FaqQue> GetByName(string name);
        Task<FaqQue> Insert(FaqQue obj);
        Task<FaqQue> Update(FaqQue obj, int id);
        Task<FaqQue> Delete(int id);
        Task Save();
    }
}
