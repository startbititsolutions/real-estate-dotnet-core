using Models.Database_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IFaqAns
    {
        Task<IEnumerable<FaqAns>> GetAll();
        Task<FaqAns> GetByPropertyId(int id);
        Task<FaqAns> GetById(int id);
        Task<FaqAns> GetByName(string name);
        Task<FaqAns> Insert(FaqAns obj);
        Task<FaqAns> Update(FaqAns obj, int id);
        Task<FaqAns> Delete(int id);
        Task Save();
    }
}
