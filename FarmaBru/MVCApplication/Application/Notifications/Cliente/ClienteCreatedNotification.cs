using MediatR;

namespace API.Application.Notifications.Cliente
{
    public class ClienteCreatedNotification : INotification
    {
        public ClienteCreatedNotification(MetaData.Entities.Cliente cliente)
        {
            Id = cliente.ID;
            Nome = cliente.Nome;
            CPF = cliente.CPF;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
    }
}
