using BusinessLogicalLayer.Extensions;
using BusinessLogicalLayer.Interfaces;
using BusinessLogicalLayer.Validators.Cliente;
using Common;
using DataAccessLayer;
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
            var response = new InsertClienteValidator().Validate(cliente).ToResponse();

            if (!response.HasSuccess)
            {
                return ResponseFactory.CreateSingleResponseFailure<Cliente>(response.Message);
            }

            using (FarmaBruContext db = new())
            {
                try
                {
                    await Task.Run(() => db.Clientes.Add(cliente));
                    await Task.Run(() => db.SaveChangesAsync());
                    return ResponseFactory.CreateSingleResponseSuccess<Cliente>(cliente);
                }
                catch (Exception ex)
                {
                    //Erros possíveis:
                    //1 - Banco fora
                    //2 - Banco lotado
                    //3 - Erro de chave única
                    return ResponseFactory.CreateSingleResponseFailure<Cliente>(ex); 
                }
            }
        }

        public async Task<Response> Deactivate(int id)
        {
            try
            {
                using var db = new FarmaBruContext();

                var response = await Task.Run(() => Get(id));
                if (!response.HasSuccess)
                {
                    return ResponseFactory.CreateFailureResponse(response);
                }

                Cliente cliente = response.Item;
                cliente.Ativo = false;

                await Task.Run(() => Update(cliente));
                await db.SaveChangesAsync();
                return new Response(true, "Cliente inativado com sucesso!");
            }
            catch (Exception ex)
            {
                return new Response(ex);
            }
        }

        public async Task<Response> Delete(int id)
        {
            try
            {
                using var db = new FarmaBruContext();
                
                var response = await Task.Run(() => Get(id));
                if (!response.HasSuccess)
                {
                    return ResponseFactory.CreateFailureResponse(response);
                }

                await Task.Run(() => db.Clientes.Remove(response.Item));
                await db.SaveChangesAsync();
                return new Response(true, "Cliente removido com sucesso");
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateFailureResponse(ex);
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
                return ResponseFactory.CreateSingleResponseFailure<Cliente>(ex);
            }
        }

        public async Task<SingleResponse<Cliente>> Get(int id)
        {
            if (id <= 0)
            {
                return ResponseFactory.CreateSingleResponseFailure<Cliente>("Id inválido.");
            }

            try
            {
                using var db = new FarmaBruContext();
                //task.run??
                //se não encontrar por id, vai retornar null
                Cliente cliente = await db.Clientes.FindAsync(id);
                if (cliente == null)
                {
                    return ResponseFactory.CreateNotFoundResponseFailure<Cliente>();
                }

                ReNormatize(cliente);
                return ResponseFactory.CreateSingleResponseSuccess<Cliente>(cliente);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateSingleResponseFailure<Cliente>(ex);
            }
        }

        public async Task<SingleResponse<Cliente>> Update(Cliente cliente)
        {
            this.Normatize(cliente);
            var response = new UpdateClienteValidator().Validate(cliente).ToResponse();

            if (!response.HasSuccess)
            {
                return ResponseFactory.CreateSingleResponseFailure<Cliente>(response.Message);
            }

            try
            {
                using var db = new FarmaBruContext();
                //db.Entry(cliente).State = EntityState.Modified;
                await Task.Run(() => db.Clientes.Update(cliente));
                await db.SaveChangesAsync();
                return ResponseFactory.CreateSingleResponseSuccess(cliente);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateSingleResponseFailure<Cliente>(ex);
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
