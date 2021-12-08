using BusinessLogicalLayer.Interfaces;
using MediatR;
using API.Application.Notifications;
using API.Application.Commands;
using Common;

namespace API.Application.Handlers.Cliente;

public class DeletaCommandHandler : IRequestHandler<ExcluiCommand, Response>
{
    private readonly IMediator _mediator;
    private readonly IRepository<MetaData.Entities.Cliente> _repository;

    public DeletaCommandHandler(IMediator mediator, IRepository<MetaData.Entities.Cliente> repository)
    {
        _mediator = mediator;
        _repository = repository;
    }

    public async Task<Response> Handle(ExcluiCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _repository.Delete(request.Id);

            //O que dá para aproveitar do baseReponse? Talvez passar como parâmetro no notification?
            if (!response.HasSuccess)
            {
                await _mediator.Publish(new DeletedNotification(request.Id, false));
                return await Task.FromResult(response);
            }

            await _mediator.Publish(new DeletedNotification(request.Id, true));
            return await Task.FromResult(response);
        }
        catch (Exception ex)
        {
            await _mediator.Publish(new DeletedNotification(request.Id, false));
            await _mediator.Publish(new ErrorNotification(ex));
            return await Task.FromResult(ResponseFactory.CreateFailureResponse(ex));
        }
    }
}
