using Common;
using MetaData;

namespace BusinessLogicalLayer.Interfaces
{
    public interface IEntityCRUD<T> where T: Entity, new()
    {
        Response Insert(T t);
        Response Update(T t);
        Response Delete(int id);
        Response Deactivate(int id);
        SingleResponse<T> GetById(int id);
        DataResponse<T> GetAll();
    }
}
