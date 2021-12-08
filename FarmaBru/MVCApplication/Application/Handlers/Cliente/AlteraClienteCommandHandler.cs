using BusinessLogicalLayer.Interfaces;
using MediatR;
using API.Application.Commands.Cliente;
using API.Application.Notifications;
using API.Application.Notifications.Cliente;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Handlers.Cliente
{
    public class AlteraClienteCommandHandler : IRequestHandler<AlteraClienteCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<MetaData.Entities.Cliente> _repository;

        public AlteraClienteCommandHandler(IMediator mediator, IRepository<MetaData.Entities.Cliente> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<string> Handle(AlteraClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = new MetaData.Entities.Cliente
            {
                ID = request.Id,
                Nome = request.Nome,
                DataNascimento = request.DataNascimento,
                Telefone = request.Telefone,
                Email = request.Email,
                CPF = request.CPF,
                Ativo = request.Ativo
            };

            try
            {
                await _repository.Update(cliente);
                await _mediator.Publish(new ClienteChangedNotification(cliente, true));
                return await Task.FromResult("Cliente alterado com sucesso");
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ClienteChangedNotification(cliente, false));
                await _mediator.Publish(new ErrorNotification(ex));
                return await Task.FromResult("Ocorreu no momento da alteração");
            }
        }
    }
}
