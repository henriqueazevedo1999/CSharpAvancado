using Common;
using MetaData.Entities;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Interfaces
{
    public interface IEntityCRUD<T> where T: Entity, new()
    {
        Task<Response> Insert(T t);
        Task<Response> Update(T t);
        Task<Response> Delete(int id);
        Task<Response> Deactivate(int id);
        Task<SingleResponse<T>> GetById(int id);
        Task<DataResponse<T>> GetAll();
    }
}
