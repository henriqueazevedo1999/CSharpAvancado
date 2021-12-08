using Common;
using MediatR;

namespace API.Application.Commands;

public class ExcluiCommand : IRequest<Response>
{
    public int Id { get; set; }
}
