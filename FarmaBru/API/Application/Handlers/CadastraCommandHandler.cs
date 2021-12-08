using BusinessLogicalLayer.Interfaces;
using MediatR;
using API.Application.Notifications;
using API.Application.Commands;
using Common;

namespace API.Application.Handlers.Cliente;

public class CadastraCommandHandler : IRequestHandler<CadastraCommand, SingleResponse<MetaData.Entities.Cliente>>
{
    private readonly IMediator _mediator;
    private readonly IRepository<MetaData.Entities.Cliente> _repository;

    public CadastraCommandHandler(IMediator mediator, IRepository<MetaData.Entities.Cliente> repository)
    {
        _mediator = mediator;
        _repository = repository;
    }

    public async Task<SingleResponse<MetaData.Entities.Cliente>> Handle(CadastraCommand request, CancellationToken cancellationToken)
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
            var response = await _repository.Insert(cliente);
            if (!response.HasSuccess)
            {
                await _mediator.Publish(new CreatedNotification(cliente));
                //await _mediator.Publish(new ErrorNotification(response));
                return await Task.FromResult(response);
            }

            await _mediator.Publish(new CreatedNotification(response.Item));
            return await Task.FromResult(response);
        }
        catch (Exception ex)
        {
            await _mediator.Publish(new ErrorNotification(ex));
            return await Task.FromResult(ResponseFactory.CreateSingleResponseFailure<MetaData.Entities.Cliente>(ex));
        }
    }
}
