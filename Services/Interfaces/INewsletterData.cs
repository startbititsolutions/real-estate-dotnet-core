using Models.Database_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public  interface INewsletterData
    {
        Task<IEnumerable<Newsletter>> GetAll();
        Task<Newsletter> GetById(int id);
        Task<Newsletter> GetByEmail(string id);

        Task<Newsletter> Insert(Newsletter obj);
        Task<Newsletter> Update(Newsletter obj, int id);
        Task<Newsletter> Delete(int id);
        Task Save();
    }
}
