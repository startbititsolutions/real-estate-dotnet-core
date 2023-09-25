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
    public class CityData : ICityData
    {
        private readonly IUnitOfWork _unitofwork;

        public CityData(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public async Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<City>> GetAll()
        {
            return await _unitofwork.cities.GetData();
        }



        public async Task<City> GetById(int id)
        {
            try
            {

                var result = await _unitofwork.cities.GetDataById(id);
                return result;
            }
            catch
            {
                return null;
            }

        }

        public async Task<City> GetByName(string name)
        {
            try
            {

                var city = await _unitofwork.cities.GetByExpression(c => c.CityName == name);
                return city;
            }
            catch (Exception ex)
            {
                return null;
            }



        }

        public async Task<City> Insert(City obj)
        {
            try
            {
                var c = await _unitofwork.cities.AddData(obj);
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

        public async Task<City> Update(City obj, string id)
        {
            throw new NotImplementedException();
        }

    }
}
