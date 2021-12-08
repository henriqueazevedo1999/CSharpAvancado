﻿using MediatR;

namespace API.Application.Notifications;

public class ChangedNotification : INotification
{
    public ChangedNotification(MetaData.Entities.Cliente cliente, bool ehEfetivado)
    {
        Id = cliente.ID;
        Nome = cliente.Nome;
        CPF = cliente.CPF;
        EhEfetivado = ehEfetivado;
    }

    public int Id { get; set; }
    public string Nome { get; set; }
    public string CPF { get; set; }
    public bool EhEfetivado { get; set; }
}
