using MediatR;

namespace API.Application.Notifications.Cliente
{
    public class ClienteDeletedNotification : INotification
    {
        public ClienteDeletedNotification(int id, bool ehEfetivado)
        {
            Id = id;    
            EhEfetivado = ehEfetivado;
        }

        public int Id { get; set; }
        public bool EhEfetivado { get; set; }
    }
}
