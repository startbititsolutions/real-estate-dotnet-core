using Models.Database_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICategoryData
    {
        Task<IEnumerable<Catagory>> GetAll();
        Task<Catagory> GetById(string id);

        Task<Catagory> GetByName(string name);
        Task<Catagory> Insert(Catagory obj);
        Task<Catagory> Update(Catagory obj, string id);
        Task Delete(string id);
        Task Save();
    }
}
