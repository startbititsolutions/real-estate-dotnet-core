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
    public class NewsletterData : INewsletterData
    {
        private readonly IUnitOfWork _unitofwork;

        public NewsletterData(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public async Task<Newsletter> Delete(int id)
        {
            try
            {
               var result= await _unitofwork.Newsletters.DeleteData(id);
                _unitofwork.commit();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Newsletter>> GetAll()
        {
            return await _unitofwork.Newsletters.GetData();
        }
        public async Task<Newsletter> GetByEmail(string id)
        {
            return await _unitofwork.Newsletters.GetByExpression(s=>s.Email.ToLower()==id.ToLower());
        }
        public async Task<Newsletter> GetById(int id)
        {
            return await _unitofwork.Newsletters.GetDataById(id);
        }

        public async Task<Newsletter> Insert(Newsletter obj)
        {
            try
            {
                var newsletter = await _unitofwork.Newsletters.AddData(obj);
                _unitofwork.commit();
                return newsletter;
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

        public async Task<Newsletter> Update(Newsletter obj, int id)
        {
            try
            {

           var result= await _unitofwork.Newsletters.GetDataById(id);
            result.Email=obj.Email;
               var final= await _unitofwork.Newsletters.EditData(result);
            _unitofwork.commit();
            return final;
            }
            catch(Exception ex)
            {
                Console.Write(ex.ToString());
                return null;
            }
        }
    }
}
