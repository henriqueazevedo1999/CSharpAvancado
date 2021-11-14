using Common;
using MetaData;

namespace BusinessLogicalLayer.Interfaces
{
    public interface IClienteService : IEntityCRUD<Cliente>
    {
        SingleResponse<Cliente> GetByCPF(string cpf);
    }
}
