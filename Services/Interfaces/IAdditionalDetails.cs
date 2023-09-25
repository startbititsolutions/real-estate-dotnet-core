using Models.Database_Models;
using Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AdditionalDetails = Models.Database_Models.AdditionalDetails;

namespace Services.Interfaces
{
    public interface IAdditionalDetails
    {
        Task<IEnumerable<AdditionalDetails>> GetAll();
        Task<AdditionalDetails> GetById(int id);
        Task<AdditionalDetails> GetByPropertyId(int id);

        Task<AdditionalDetails> Insert(AdditionalDetails obj);
        void Update(AdditionalDetails obj, string id);
        Task Delete(string id);
        Task Save();
    }
}
