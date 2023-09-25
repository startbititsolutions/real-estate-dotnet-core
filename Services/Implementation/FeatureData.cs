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
    public class FeatureData : IFeatureData
    {
        private readonly IUnitOfWork _unitofwork;

        public FeatureData(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public async Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Features>> GetAll()
        {
            return await _unitofwork.features.GetData();
        }

        public async Task<Features> GetByPropertyId(int id)
        {
            return await _unitofwork.features.GetByExpression(s=>s.PropertyId==id);
        }

        public async Task<Features> GetById(int id)
        {
            return await _unitofwork.features.GetDataById(id);
        }

        public async Task<Features> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Features> Insert(Features obj)
        {
            try
            {
                var c = await _unitofwork.features.AddData(obj);
                _unitofwork.commit();
                return c;
            }
            catch(Exception e)
            {
                return null;
            }

        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public async Task<Features> Update(Features obj, string id)
        {
            throw new NotImplementedException();
        }
    }
}
