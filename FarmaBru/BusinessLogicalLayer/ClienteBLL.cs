using BusinessLogicalLayer.Extensions;
using BusinessLogicalLayer.Interfaces;
using BusinessLogicalLayer.Validators.ClienteValidator;
using Common;
using DataAccessLayer;
using FluentValidation.Results;
using MetaData;
using MetaData.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer
{
    public class ClienteBLL : BaseValidator<Cliente>, IClienteService
    {
        public async Task<Response> Insert(Cliente cliente)
        {
            this.Normatize(cliente);
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

                    //TODO: Mensagem padrão vir de outro lugar
                    if (ex.InnerException.Message.Contains("UQ_CLIENTE_EMAIL"))
                    {
                        return new Response
                        {
                            HasSuccess = false,
                            Message = "Email já cadastrado.",
                            Exception = ex
                        };
                    }

                    if (ex.InnerException.Message.Contains("UQ_CLIENTE_CPF"))
                    {
                        return new Response
                        {
                            HasSuccess = false,
                            Message = "CPF já cadastrado.",
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
            try
            {
                using (FarmaBruContext db = new FarmaBruContext())
                {
                    List<Cliente> clientes = await db.Clientes.Where(c => c.Ativo).ToListAsync();

                    //var query = db.Clientes.Select(c => new
                    //{
                    //    Nome = c.Nome,
                    //    Data = c.DataNascimento
                    //});

                    clientes.ForEach(cliente => this.ReNormatize(cliente));

                    return new DataResponse<Cliente>()
                    {
                        HasSuccess = true,
                        Message = "Dados selecionados com sucesso",
                        Data = clientes
                    };
                }
            }
            catch (Exception ex)
            {
                //aplicar factory
                return new DataResponse<Cliente>()
                {
                    HasSuccess = true,
                    Message = "Erro no banco de dados, contate o administrador",
                    Exception = ex
                };
            }
        }

        public async Task<SingleResponse<Cliente>> GetByCPF(string cpf)
        {
            throw new NotImplementedException();
        }

        public async Task<SingleResponse<Cliente>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> Update(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        protected override void Normatize(Cliente item)
        {
            item.Nome = item.Nome.Normatize();
            item.Telefone = item.Telefone.RemoveMask();
            item.CPF = item.CPF.RemoveMask();
        }

        protected override void ReNormatize(Cliente item)
        {
            item.CPF = item.CPF.FormatAsCPF();
        }
    }
}
