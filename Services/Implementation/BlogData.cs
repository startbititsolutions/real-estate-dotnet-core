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
    public class BlogData : IBlogData

    {

        private readonly IUnitOfWork _unitofwork;

        public BlogData(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public async Task<Blog> Delete(int id)
        {
            try
            {
                var result = await _unitofwork.Blog.DeleteData(id);
                _unitofwork.commit();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Blog>> GetAll()
        {
            return await _unitofwork.Blog.GetData();
        }

        public async Task<Blog> GetById(int id)
        {
            return await _unitofwork.Blog.GetDataById(id);
        }

        public async Task<Blog> Insert(Blog obj)
        {
            try
            {
                var c = await _unitofwork.Blog.AddData(obj);
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

        public async Task<Blog> Update(Blog obj, int id)
        {
            var result = await _unitofwork.Blog.GetDataById(id);
            result.Title = obj.Title;
            result.AuthorName = obj.AuthorName;
            result.BlogName = obj.BlogName;
            result.BlogImg = obj.BlogImg;


            result.Summary = obj.Summary;
            result.BlogDesc = obj.BlogDesc;
            var final = await _unitofwork.Blog.EditData(result);
            _unitofwork.commit();
            return final;
        }
    }
}
