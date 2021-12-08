using MediatR;
using System;

namespace API.Application.Commands.Cliente
{
    public class AlteraClienteCommand : IRequest<string>
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; }
    }
}
