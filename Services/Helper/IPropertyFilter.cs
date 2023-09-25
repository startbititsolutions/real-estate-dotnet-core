using Models.Database_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers
{
    public interface IPropertyFilter
    {
        Task<IEnumerable<Property>> Filter(string formcollection);
        Task<int> FilterCount(string formCollection);
    }
}
