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
    public class DeletaClienteCommandHandler : IRequestHandler<ExcluiClienteCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<MetaData.Entities.Cliente> _repository;

        public DeletaClienteCommandHandler(IMediator mediator, IRepository<MetaData.Entities.Cliente> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<string> Handle(ExcluiClienteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _repository.Delete(request.Id);

                //O que dá para aproveitar do baseReponse? Talvez passar como parâmetro no notification?
                if (!response.HasSuccess)
                {
                    await _mediator.Publish(new ClienteDeletedNotification(request.Id, false));
                    return await Task.FromResult("Não foi possível excluir cliente");
                }

                await _mediator.Publish(new ClienteDeletedNotification(request.Id, true));
                return await Task.FromResult("Cliente excluído com sucesso");
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ClienteDeletedNotification(request.Id, false));
                await _mediator.Publish(new ErrorNotification(ex));
                return await Task.FromResult("Não foi possível excluir cliente");
            }
        }
    }
}
