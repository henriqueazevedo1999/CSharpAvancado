using MediatR;

namespace API.Application.Notifications;

public class CreatedNotification : INotification
{
    public CreatedNotification(MetaData.Entities.Cliente cliente)
    {
        Id = cliente.ID;
        Nome = cliente.Nome;
        CPF = cliente.CPF;
    }

    public int Id { get; set; }
    public string Nome { get; set; }
    public string CPF { get; set; }
}
