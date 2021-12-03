using BusinessLogicalLayer.Extensions;
using BusinessLogicalLayer.Interfaces;
using BusinessLogicalLayer.Validators.Cliente;
using Common.Response;
using DataAccessLayer;
using FluentValidation.Results;
using MetaData.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogicalLayer
{
    public class ClienteBLL : BaseValidator<Cliente>, IClienteService
    {
        public async Task<SingleResponse<Cliente>> Insert(Cliente cliente)
        {
            this.Normatize(cliente);
            InsertClienteValidator validator = new();
            ValidationResult result = validator.Validate(cliente);

            IResponse response = result.ToResponse();
            if (!response.HasSuccess)
            {
                return new SingleResponse<Cliente>(response);
            }

            using (FarmaBruContext db = new())
            {
                try
                {
                    await Task.Run(() => db.Clientes.Add(cliente));
                    await Task.Run(() => db.SaveChangesAsync());
                    return new SingleResponse<Cliente>(true, "Cliente inserido com sucesso!");
                }
                catch (Exception ex)
                {
                    //Erros possíveis:
                    //1 - Banco fora
                    //2 - Banco lotado
                    //3 - Erro de chave única
                    return new SingleResponse<Cliente>(ex); 
                }
            }
        }

        public async Task<SingleResponse<Cliente>> Deactivate(int id)
        {
            try
            {
                using var db = new FarmaBruContext();

                var response = await Task.Run(() => Get(id));
                if (!response.HasSuccess)
                {
                    return response;
                }

                Cliente cliente = response.Item;
                cliente.Ativo = false;

                await Task.Run(() => Update(cliente));
                await db.SaveChangesAsync();
                return new SingleResponse<Cliente>(true, "Cliente inativado com sucesso!");
            }
            catch (Exception ex)
            {
                return new SingleResponse<Cliente>(ex);
            }
        }

        public async Task<SingleResponse<Cliente>> Delete(int id)
        {
            try
            {
                using var db = new FarmaBruContext();
                
                var response = await Task.Run(() => Get(id));
                if (!response.HasSuccess)
                {
                    return response;
                }

                await Task.Run(() => db.Clientes.Remove(response.Item));
                await db.SaveChangesAsync();
                return new SingleResponse<Cliente>(true, "Cliente removido com sucesso");
            }
            catch (Exception ex)
            {
                return new SingleResponse<Cliente>(ex);
            }
        }

        public async Task<DataResponse<Cliente>> GetAll()
        {
            try
            {
                using FarmaBruContext db = new FarmaBruContext();
                List<Cliente> clientes = await Task.Run(() => db.Clientes.Where(c => c.Ativo).ToListAsync());

                //var query = db.Clientes.Select(c => new
                //{
                //    Nome = c.Nome,
                //    Data = c.DataNascimento
                //});

                clientes.ForEach(cliente => this.ReNormatize(cliente));

                return new DataResponse<Cliente>(true, "Dados selecionados com sucesso")
                {
                    Data = clientes
                };
            }
            catch (Exception ex)
            {
                return new DataResponse<Cliente>(ex);
            }
        }

        public async Task<SingleResponse<Cliente>> GetByCPF(string cpf)
        {
            try
            {
                using FarmaBruContext db = new FarmaBruContext();
                Cliente cliente = await Task.Run(() => db.Clientes.First(c => c.CPF == cpf));
                ReNormatize(cliente);

                return new SingleResponse<Cliente>(true, "Cliente selecionado com sucesso");
            }
            catch (Exception ex)
            {
                return new SingleResponse<Cliente>(ex);
            }
        }

        public async Task<SingleResponse<Cliente>> Get(int id)
        {
            try
            {
                using var db = new FarmaBruContext();
                Cliente cliente = await Task.Run(() => db.Clientes.First(c => c.ID == id));

                //null ou exception?
                if (cliente == null)
                {
                    return new SingleResponse<Cliente>(false, "Cliente não encontrado");
                }

                ReNormatize(cliente);
                return new SingleResponse<Cliente>(true, "Cliente selecionado com sucesso");
            }
            catch (Exception ex)
            {
                return new SingleResponse<Cliente>(ex);
            }
        }

        public async Task<SingleResponse<Cliente>> Update(Cliente cliente)
        {
            this.Normatize(cliente);
            var validator = new InsertClienteValidator();
            ValidationResult result = validator.Validate(cliente);

            IResponse response = result.ToResponse();
            if (!response.HasSuccess)
            {
                return new SingleResponse<Cliente>(response);
            }

            try
            {
                using var db = new FarmaBruContext();
                await Task.Run(() => db.Clientes.Update(cliente));
                await db.SaveChangesAsync();
                return new SingleResponse<Cliente>(true, "Cliente atualizado com suceso");
            }
            catch (Exception ex)
            {
                return new SingleResponse<Cliente>(ex);
            }
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
