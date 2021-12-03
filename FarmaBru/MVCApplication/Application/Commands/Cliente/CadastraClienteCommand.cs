using MediatR;
using System;

namespace MVCApplication.Application.Commands.Cliente
{
    public class CadastraClienteCommand : IRequest<string>
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
