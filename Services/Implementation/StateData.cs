
using DataAccess;
using Models.Database_Models;

using Services.Interfaces;
using System.Threading.Tasks;


namespace services.implementation
{
    public class statedata : IStateData
    {
        private readonly IUnitOfWork _unitofwork;

        public statedata(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public async Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<State>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<State> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<State> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<State> Insert(State obj)
        {
            try
            {
                var a = await _unitofwork.states.AddData(obj);
                _unitofwork.commit();
                return a;
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

        public async Task<State> Update(State obj, string id)
        {
            throw new NotImplementedException();
        }
    }
}
