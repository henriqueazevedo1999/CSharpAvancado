using MediatR;

namespace API.Application.Commands.Cliente
{
    public class ExcluiClienteCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
}
