using DataAccess;
using Models.Database_Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class AdditionalDetails : IAdditionalDetails
    {
        private readonly IUnitOfWork _unitofwork;

        public AdditionalDetails(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public async Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Models.Database_Models.AdditionalDetails> Insert(Models.Database_Models.AdditionalDetails obj)
        {
            var result = await _unitofwork.additionaldetails.AddData(obj);

            _unitofwork.commit();
            return result;
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public void Update(AdditionalDetails obj, string id)
        {
            throw new NotImplementedException();
        }

        public void Update(Models.Database_Models.AdditionalDetails obj, string id)
        {
            throw new NotImplementedException();
        }

        async Task<IEnumerable<Models.Database_Models.AdditionalDetails>> IAdditionalDetails.GetAll()
        {
            return await _unitofwork.additionaldetails.GetData();
        }

        async  Task<Models.Database_Models.AdditionalDetails> IAdditionalDetails.GetById(int id)
        {
            return await _unitofwork.additionaldetails.GetDataById(id);
        }

        public async Task<Models.Database_Models.AdditionalDetails> GetByPropertyId(int id)
        {
             return await _unitofwork.additionaldetails.GetByExpression(s => s.PropertyId == id);
        }
    }
}
