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
    public class StatusData : IStatusData
    {
        private readonly IUnitOfWork _unitofwork;

        public StatusData(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public async Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Status>> GetAll()
        {
            return await _unitofwork.status.GetData();
        }

        public async Task<Status> GetById(int id)
        {
            return await _unitofwork.status.GetDataById(id);
        }

        public async Task<Status> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Status> Insert(Status obj)
        {
            try
            {
                var a = await _unitofwork.status.AddData(obj);
                _unitofwork.commit();
                return a;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public async Task<Status> GetByPropertyId(int id)
        {
            throw new NotImplementedException();
           // return await _unitofwork.status.GetByExpression(s => s.PropertyId == id);
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public async Task<Status> Update(Status obj, string id)
        {
            throw new NotImplementedException();
        }
    }
}
