using Models.Database_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IUnitOfWork
    {
        IRepository<Property> properties { get; }
        IRepository<Catagory> categories { get; }
        IRepository<Status> status { get; }
        IRepository<City> cities { get; }
        IRepository<State> states { get; }
        IRepository<Features> features { get; }
        //IRepository<PropertyFeatures> propertyfeature { get; }
        IRepository<PropertyImage> propertyimages { get; }
        IRepository<AdditionalDetails> additionaldetails { get; }
        IRepository<Banner> banners { get; }
        IRepository<Logo> logos { get; }
        IRepository<Contact> contacts { get; }
        IRepository<Map> maps { get; }
        IRepository<Newsletter> Newsletters { get; }
        IRepository<FaqQue> FaqQue { get; }
        IRepository<FaqAns> FaqAns { get; }
        IRepository<Blog> Blog { get; }
        void commit();


    }
}
