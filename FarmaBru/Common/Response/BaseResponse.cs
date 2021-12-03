using System;

namespace Common.Response
{
    //TODO: Bem ruim assim, ver como melhorar, talvez um factory?
    public abstract class BaseResponse : IResponse
    {
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
    }
}
