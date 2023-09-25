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
    public class MapData : IMapData
    {
        private readonly IUnitOfWork _unitofwork;

        public MapData(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public async Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Map>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Map> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Map> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Map> Insert(Map obj)
        {
            try
            {
                var a = await _unitofwork.maps.AddData(obj);
                _unitofwork.commit();
                return a;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public async Task<Map> Update(Map obj, string id)
        {
            throw new NotImplementedException();
        }
    }
}
