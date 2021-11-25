using BusinessLogicalLayer.Extensions;
using BusinessLogicalLayer.Interfaces;
using BusinessLogicalLayer.Validators.ClienteValidator;
using Common;
using DataAccessLayer;
using FluentValidation.Results;
using MetaData;
using MetaData.Entities;
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
        public async Task<Response> Deactivate(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<DataResponse<Cliente>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<SingleResponse<Cliente>> GetByCPF(string cpf)
        {
            throw new NotImplementedException();
        }

        public async Task<SingleResponse<Cliente>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> Insert(Cliente cliente)
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

                try
                {
                    await db.SaveChangesAsync();
                    return new Response
                    {
                        HasSuccess = true,
                        Message = "Cliente inserido com sucesso!"
                    };
                }
                catch (Exception ex)
                {
                    //Erros possíveis:
                    //1 - Banco fora
                    //2 - Banco lotado
                    //3 - Erro de chave única
                             
                    if (ex.InnerException.Message.Contains("UQ_CLIENTE_EMAIL"))
                    {
                        return new Response
                        {
                            HasSuccess = false,
                            Message = "Email já cadastrado.",
                            Exception = ex
                        };
                    }

                    return new Response
                    {
                        HasSuccess = false,
                        Message = "Erro no banco de dados, contate o administrador",
                        Exception = ex
                    };

                }
            }
        }

        public async Task<Response> Update(Cliente cliente)
        {
            throw new NotImplementedException();
        }
    }
}
