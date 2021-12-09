using Common;
using MediatR;
using MetaData.Entities;

namespace ClienteAPI.Application.Queries;

public class GetByIdQuery : IRequest<SingleResponse<Cliente>>
{
    public int? Id { get; set; }
}