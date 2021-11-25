using Common;
using MetaData.Entities;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Interfaces
{
    public interface IClienteService : IEntityCRUD<Cliente>
    {
        Task<SingleResponse<Cliente>> GetByCPF(string cpf);
    }
}
