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
    public class FaqQue : IFaqQue
    {
        private readonly IUnitOfWork _unitofwork;

        public FaqQue(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public async Task<Models.Database_Models.FaqQue> Delete(int id)
        {
            try
            {
               var result=await _unitofwork.FaqQue.DeleteData(id);
                _unitofwork.commit();
                return result;
            }catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<IEnumerable<Models.Database_Models.FaqQue>> GetAll()
        {
            return await _unitofwork.FaqQue.GetData();
        }

       public async Task<Models.Database_Models.FaqQue> GetById(int id)
        {
            return await _unitofwork.FaqQue.GetDataById(id);
        }

        public Task<Models.Database_Models.FaqQue> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Database_Models.FaqQue> Insert(Models.Database_Models.FaqQue obj)
        {
            try
            {
                var result = _unitofwork.FaqQue.AddData(obj);
                _unitofwork.commit();
                return result;
            }
            catch(Exception ex) {
                Console.Write(ex.Message);
                return null;
            }

        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public async Task<Models.Database_Models.FaqQue> Update(Models.Database_Models.FaqQue obj, int id)
        {
            try
            {

           var existfaqques=await _unitofwork.FaqQue.GetDataById(id);
            existfaqques.que = obj.que;
            var result=await _unitofwork.FaqQue.EditData(existfaqques);
           _unitofwork.commit();
            return result;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}

     
    

