using Models.Database_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPropertyData
    {
        Task<IEnumerable<Property>> GetAll();
        Task<Property> GetById(int id);

        Task<Property> GetByName(string name);
        Task<Property> Insert(Property obj);
        Task<Property> Update(Property obj, int id);
        Task<Property> Delete(int id);
        Task<Property> GetMax(Expression<Func<Property, int>> keyselector);
        Task<Property> GetMin(Expression<Func<Property, int>> keyselector);
        Task<IEnumerable<Property>> GetByAgentName(string name);
        Task<int> Count();
        Task<IEnumerable<Property>> GetTop(int n);
        Task<IEnumerable<Property>> GetActiveProperties();
        Task Save();
    }
}
