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
    public class CategoryData : ICategoryData
    {
        private readonly IUnitOfWork _unitofwork;

        public CategoryData(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public async Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        

       

        public async Task<Catagory> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Catagory> Insert(Catagory obj)
        {
            try
            {
                var c = await _unitofwork.categories.AddData(obj);
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

        public async Task<Catagory> Update(Catagory obj, string id)
        {
            throw new NotImplementedException();
        }

        

       async Task<IEnumerable<Catagory>> ICategoryData.GetAll()
        {
           return await _unitofwork.categories.GetData();
        }

       async Task<Catagory> ICategoryData.GetById(string id)
        {
            throw new NotImplementedException();
        }

       async Task<Catagory> ICategoryData.GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
