using DataAccess;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class FaqAns : IFaqAns
    {
        private readonly IUnitOfWork _unitofwork;

        public FaqAns(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public async Task<Models.Database_Models.FaqAns> Delete(int id)
        {
            try
            {
              var result= await _unitofwork.FaqAns.DeleteData(id);
                _unitofwork.commit();
                return result;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }

        }

        public async Task<IEnumerable<Models.Database_Models.FaqAns>> GetAll()
        {
            return await _unitofwork.FaqAns.GetData();
        }

        public async Task<Models.Database_Models.FaqAns> GetById(int id)
        {
            return await _unitofwork.FaqAns.GetDataById(id);
        }

        public Task<Models.Database_Models.FaqAns> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Models.Database_Models.FaqAns> GetByPropertyId(int id)
        {
            return await _unitofwork.FaqAns.GetDataById(id);
        }

        public async Task<Models.Database_Models.FaqAns> Insert(Models.Database_Models.FaqAns obj)
        {
            var result = await _unitofwork.FaqAns.AddData(obj);

            _unitofwork.commit();
            return result;
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public async Task<Models.Database_Models.FaqAns> Update(Models.Database_Models.FaqAns obj, int id)
        {
            try
            {

            var existfaq = await _unitofwork.FaqAns.GetDataById(id);
            existfaq.ans=obj.ans;
            
            var result=await _unitofwork.FaqAns.EditData(existfaq);
                _unitofwork.commit();
            return result;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}

