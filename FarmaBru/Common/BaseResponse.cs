using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Common
{
    //TODO: Bem ruim assim, ver como melhorar, talvez um factory?
    public class BaseResponse : IResponse
    {
        private readonly List<string> messages = new List<string>();

        protected BaseResponse()
        {
        }

        public BaseResponse(bool hasSuccess, string message)
        {
            this.HasSuccess = hasSuccess;
            this.Message = message;
        }

        public BaseResponse(Exception ex)
        {
            this.Exception = ex;
            this.HasSuccess = false;
            this.Message = GetExceptionMessage();
        }

        public string Message { get; set; }
        public bool HasSuccess { get; set; }
        public Exception Exception { get; set; }
        public IEnumerable<string> Errors
        {
            get
            {
                return new ReadOnlyCollection<string>(messages);
            }
        }

        private string GetExceptionMessage()
        {
            if (this.Exception.InnerException.Message.Contains("UQ_CLIENTE_EMAIL"))
            {
                return "Email já cadastrado.";
            }

            if (this.Exception.InnerException.Message.Contains("UQ_CLIENTE_CPF"))
            {
                return "CPF já cadastrado.";
            }

            return "Erro no banco de dados, contate o administrador";
        }

        public BaseResponse AddError(string message)
        {
            messages.Add(message);
            return this;
        }
    }
}
