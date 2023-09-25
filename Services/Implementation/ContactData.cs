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
    public class ContactData : IContactData
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactData(IUnitOfWork unitofwork)
        {
            _unitOfWork = unitofwork;
        }
        public async Task<IEnumerable<Contact>> GetAll()
        {
            return await _unitOfWork.contacts.GetData();
        }

        public async Task<Contact> GetById(int id)
        {
            return await _unitOfWork.contacts.GetDataById(id);
        }

        public async Task<Contact> GetByName(string name)
        {
            try
            {

                var FirstName = await _unitOfWork.contacts.GetByExpression(c => c.FirstName == name);
                return FirstName;
            }
            catch (Exception ex)
            {
                return null;
            }



        }


        public async Task<Contact> Insert(Contact obj)
        {
            try
            {
                var c = await _unitOfWork.contacts.AddData(obj);
                _unitOfWork.commit();
                return c;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Contact> Update(Contact obj, int id)
        {
            var result = await _unitOfWork.contacts.GetDataById(id);
            result.FirstName = obj.FirstName;
            result.LastName = obj.LastName;

            result.Email = obj.Email;
            result.Subject = obj.Subject;
            result.Message = obj.Message;
            var final = await _unitOfWork.contacts.EditData(result);
            _unitOfWork.commit();
            return final;
        }

        public async Task<Contact> Delete(int id)
        {

            try
            {
                var result = await _unitOfWork.contacts.DeleteData(id);
                _unitOfWork.commit();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }
    }
}
