using DataAccess;
using Models.Database_Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Services.Implementation
{
    public class PropertyImageData : IPropertyImageData
    {
        private readonly IUnitOfWork _unitofwork;

        public PropertyImageData(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public async Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PropertyImage>> GetAll()
        {
            return await _unitofwork.propertyimages.GetData();
        }
        public async Task<IEnumerable<PropertyImage>> GetByPropertyId(int id)
        {
            return await _unitofwork.propertyimages.GetByAllExpression(s => s.PropertyId == id);
        }
        public async Task<PropertyImage> GetById(int id)
        {
            return await _unitofwork.propertyimages.GetDataById(id);
        }

        public async Task<PropertyImage> Insert(PropertyImage obj)
        {
            try
            {
                var a=await _unitofwork.propertyimages.AddData(obj);
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

        public async Task<PropertyImage> Update(PropertyImage obj, string id)
        {
            throw new NotImplementedException();
        }
    }
}
