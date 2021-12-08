using Common;
using MetaData.Entities;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Interfaces
{
    public interface IRepository<T> where T: Entity, new()
    {
        Task<SingleResponse<T>> Insert(T t);
        Task<SingleResponse<T>> Update(T t);
        Task<Response> Delete(int id);
        Task<Response> Deactivate(int id);
        Task<SingleResponse<T>> Get(int id);
        Task<DataResponse<T>> GetAll();
    }
}
