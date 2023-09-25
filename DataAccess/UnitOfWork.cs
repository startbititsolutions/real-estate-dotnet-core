
using DataAccess.Context;
using Models.Database_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        private Repository<Property> _properties;

        private Repository<Catagory> _categories;
        private Repository<Status> _status;
        private Repository<City> _cities;
        private Repository<State> _states;
        private Repository<Features> _features;
        //private Repository<Property_Feature> _propertyfeatures;
        private Repository<PropertyImage> _propertyimages;
        private Repository<AdditionalDetails> _additionaldetails;
        private Repository<Banner> _banners;
        private Repository<Logo> _logos;
        private Repository<Contact> _contacts;
        private Repository<Map> _maps;
        private Repository<Newsletter> _newsletter;
        private Repository<FaqQue> _faqque;
        private Repository<FaqAns> _faqans;
        private Repository<Blog> _blog;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public IRepository<Property> properties
        {
            get
            {
                return _properties ??
                    (_properties = new Repository<Property>(_context));
            }
        }




        public IRepository<Catagory> categories
        {
            get
            {
                return _categories ??
                    (_categories = new Repository<Catagory>(_context));
            }
        }



        public IRepository<Status> status
        {
            get
            {
                return _status ??
                    (_status = new Repository<Status>(_context));
            }
        }



        public IRepository<City> cities
        {
            get
            {
                return _cities ??
                    (_cities = new Repository<City>(_context));
            }
        }


        public IRepository<State> states
        {
            get
            {
                return _states ??
                    (_states = new Repository<State>(_context));
            }
        }



        public IRepository<Features> features
        {
            get
            {
                return _features ??
                    (_features = new Repository<Features>(_context));
            }
        }




        public IRepository<PropertyImage> propertyimages
        {
            get
            {
                return _propertyimages ??
                    (_propertyimages = new Repository<PropertyImage>(_context));
            }
        }



        public IRepository<AdditionalDetails> additionaldetails
        {
            get
            {
                return _additionaldetails ??
                    (_additionaldetails = new Repository<AdditionalDetails>(_context));
            }
        }


        public IRepository<Banner> banners
        {
            get
            {
                return _banners ??
                    (_banners = new Repository<Banner>(_context));
            }
        }



        public IRepository<Logo> logos
        {
            get
            {
                return _logos ??
                    (_logos = new Repository<Logo>(_context));
            }
        }



        public IRepository<Contact> contacts
        {
            get
            {
                return _contacts ??
                    (_contacts = new Repository<Contact>(_context));
            }
        }



        public IRepository<Map> maps
        {
            get
            {
                return _maps ??
                    (_maps = new Repository<Map>(_context));
            }
        }
        public IRepository<Newsletter> Newsletters
        {
            get
            {
                return _newsletter ??
                    (_newsletter = new Repository<Newsletter>(_context));
            }
        }
        public IRepository<FaqQue> FaqQue
        {
            get
            {
                return _faqque ??
                    (_faqque = new Repository<FaqQue>(_context));
            }
        }
        public IRepository<FaqAns> FaqAns
        {
            get
            {
                return _faqans ??
                    (_faqans = new Repository<FaqAns>(_context));
            }
        }
        public IRepository<Blog> Blog
        {
            get
            {
                return _blog ??
                    (_blog = new Repository<Blog>(_context));
            }
        }
        public void commit()
        {
            _context.SaveChanges();
        }
    }
}
