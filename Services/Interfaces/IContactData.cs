using Models.Database_Models;
using Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IContactData
    {
        Task<IEnumerable<Contact>> GetAll();
        Task<Contact> GetById(int id);
        Task<Contact> GetByName(string name);
        Task<Contact> Insert(Contact obj);
        Task<Contact> Update(Contact obj, int id);
        Task<Contact> Delete(int id);
        Task Save();


    }
}
