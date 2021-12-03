using BusinessLogicalLayer.Interfaces;
using MediatR;
using MVCApplication.Application.Commands.Cliente;
using MVCApplication.Application.Notifications;
using MVCApplication.Application.Notifications.Cliente;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MVCApplication.Application.Handlers.Cliente
{
    public class CadastraClienteCommandHandler : IRequestHandler<CadastraClienteCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<MetaData.Entities.Cliente> _repository;

        public CadastraClienteCommandHandler(IMediator mediator, IRepository<MetaData.Entities.Cliente> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<string> Handle(CadastraClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = new MetaData.Entities.Cliente
            {
                Nome = request.Nome,
                CPF = request.CPF,
                DataNascimento = request.DataNascimento,
                Email = request.Email,
                Telefone = request.Telefone,
            };

            try
            {
                await _repository.Insert(cliente);
                await _mediator.Publish(new ClienteCreatedNotification(cliente));
                return await Task.FromResult("Cliente criado com sucesso");
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ClienteCreatedNotification(cliente));
                await _mediator.Publish(new ErrorNotification(ex));
                return await Task.FromResult("Ocorreu um erro no momento da criação");
            }
        }
    }
}
