using BusinessLogicalLayer.Extensions;
using BusinessLogicalLayer.Interfaces;
using BusinessLogicalLayer.Validators.ClienteValidator;
using Common;
using DataAccessLayer;
using FluentValidation.Results;
using MetaData;
using Microsoft.Data.SqlClient;
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

        public Response Insert(Cliente cliente)
        {
            InsertClienteValidator validator = new();
            ValidationResult result = validator.Validate(cliente);

            Response response = result.ToResponse();
            if (!response.HasSuccess)
            {
                return response;
            }

            using (FarmaBruContext db = new())
            {
                db.Clientes.Add(cliente);
                db.SaveChanges();
            }
        }

        public Response Update(Cliente cliente)
        {
            throw new NotImplementedException();
        }
    }
}
