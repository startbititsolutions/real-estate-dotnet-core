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
    public class BannerData : IBannerData
    {
        private readonly IUnitOfWork _unitofwork;

        public BannerData(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public async Task<Banner> Delete(int id)
        {
            try
            {
                var result = await _unitofwork.banners.DeleteData(id);
                _unitofwork.commit();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Banner>> GetAll()
        {
            return await _unitofwork.banners.GetData();
        }

        public async Task<Banner> GetById(int id)
        {
            return await _unitofwork.banners.GetDataById(id);
        }

        public async Task<Banner> Insert(Banner obj)
        {
            try
            {
                var c = await _unitofwork.banners.AddData(obj);
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

        public async Task<Banner> Update(Banner obj, int id)
        {
            var result = await _unitofwork.banners.GetDataById(id);
            result.BannerName = obj.BannerName;
            result.BannerUrl = obj.BannerUrl;

            var final = await _unitofwork.banners.EditData(result);
            _unitofwork.commit();
            return final;
        }


        //Banner IBannerData.GetById(string id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
