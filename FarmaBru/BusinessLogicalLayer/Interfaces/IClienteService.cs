using Common;
using MetaData.Entities;

namespace BusinessLogicalLayer.Interfaces
{
    public interface IClienteService : IEntityCRUD<Cliente>
    {
        SingleResponse<Cliente> GetByCPF(string cpf);
    }
}
