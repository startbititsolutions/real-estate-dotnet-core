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
    public class LogoData : ILogoData
    {
        private readonly IUnitOfWork _unitofwork;

        public LogoData(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public async Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Logo>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Logo> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Logo> Insert(Logo obj)
        {
            try
            {
                var c = await _unitofwork.logos.AddData(obj);
                _unitofwork.commit();
                return c;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public async Task<Logo> Update(Logo obj, string id)
        {
            throw new NotImplementedException();
        }
    }
}
