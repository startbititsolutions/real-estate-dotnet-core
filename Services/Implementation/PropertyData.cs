using DataAccess;
using Models.Database_Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class PropertyData : IPropertyData
    {
        private readonly IUnitOfWork _unitofwork;

        public PropertyData(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public async Task<Property> Delete(int id)
        {
            try
            {
                var result= await _unitofwork.properties.DeleteData(id);
                _unitofwork.commit();
                return result;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<IEnumerable<Property>> GetAll()
        {
            return await _unitofwork.properties.GetData();
        }

        public async Task<Property> GetById(int id)
        {
            return await _unitofwork.properties.GetDataById(id);
        }

        public async Task<Property> GetByName(string name)
        {
            throw new NotImplementedException();
        }
        public async Task<Property> GetMax(Expression<Func<Property, int>> keyselector)
        {
            var result = await _unitofwork.properties.GetMax(keyselector);
            return result;
        }
        public async Task<Property> GetMin(Expression<Func<Property, int>> keyselector)
        {
            var result = await _unitofwork.properties.GetMin(keyselector);
            return result;
        }
        public async Task<Property> Insert(Property obj)
        {
            var result= await _unitofwork.properties.AddData(obj);
            
            _unitofwork.commit();
            return result;
           
        }
        public async Task<IEnumerable<Property>> GetActiveProperties()
        {
            try
            {
                var activeProperties = await _unitofwork.properties.GetByAllExpression(c => c.IsActive);
                return activeProperties;
            }
            catch (Exception ex)
            {
                // Handle exception or log error
                return Enumerable.Empty<Property>();
            }
        }
        public Task Save()
        {
            throw new NotImplementedException();
        }

        public async Task<Property> Update(Property obj, int id)
        {
            try
            {
                var property = await _unitofwork.properties.GetDataById(id);

                if (property != null)
                {

                    property.IsActive = obj.IsActive;


                    var result = await _unitofwork.properties.EditData(property);
                    _unitofwork.commit();

                    return result;
                }
                else
                {
                    // Handle the case when the property is not found
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public async Task<int> Count()
        {
            try
            {
                return await _unitofwork.properties.GetCount();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;

            }
        }
        public async Task<IEnumerable<Property>> GetTop(int n)
        {
            try
            {
                return await _unitofwork.properties.GetTopN(n);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public async Task<IEnumerable<Property>> GetByAgentName(string name)
        {
            try
            {

                var property = await _unitofwork.properties.GetByAllExpression(c => c.ApplicationUser.UserName == name);
                return property;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

    }
}
