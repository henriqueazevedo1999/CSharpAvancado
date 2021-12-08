using MediatR;
using System;

namespace API.Application.Notifications
{
    public class ErrorNotification : INotification
    {
        public ErrorNotification(Exception ex)
        {
            ExceptionMessage = ex.Message;
            StackTrace = ex.StackTrace;
        }

        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }
    }
}
