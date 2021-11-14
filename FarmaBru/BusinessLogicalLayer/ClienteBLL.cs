using BusinessLogicalLayer.Interfaces;
using Common;
using MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer
{
    public class ClienteBLL : IClienteService
    {
        public Response Deactivate(int id)
        {
            throw new NotImplementedException();
        }

        public Response Delete(int id)
        {
            throw new NotImplementedException();
        }

        public DataResponse<Cliente> GetAll()
        {
            throw new NotImplementedException();
        }

        public SingleResponse<Cliente> GetByCPF(string cpf)
        {
            throw new NotImplementedException();
        }

        public SingleResponse<Cliente> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Response Insert(Cliente t)
        {
            throw new NotImplementedException();
        }

        public Response Update(Cliente t)
        {
            throw new NotImplementedException();
        }
    }
}
