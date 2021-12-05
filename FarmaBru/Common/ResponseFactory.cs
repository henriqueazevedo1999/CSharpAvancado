using System;

namespace Common
{
    public class ResponseFactory
    {
        public static Response CreateSuccessResponse()
        {
            return new Response(true, "Operação realizada com sucesso!");
        }

        public static Response CreateFailureResponse(Exception ex)
        {
            return new Response(ex);
        }

        public static Response CreateFailureResponse<T>(SingleResponse<T> singleResponse)
        {
            return new Response(false, singleResponse.Message);
        }

        public static SingleResponse<T> CreateSingleResponseFailure<T>(Exception ex)
        {
            return new SingleResponse<T>(ex);
        }

        public static SingleResponse<T> CreateSingleResponseFailure<T>(string message)
        {
            return new SingleResponse<T>(false, message);
        }

        public static SingleResponse<T> CreateSingleResponseSuccess<T>(T item)
        {
            return new SingleResponse<T>(true, "Operação realizada com sucesso.")
            {
                Item = item
            };
        }

        public static SingleResponse<T> CreateNotFoundResponseFailure<T>()
        {
            return new SingleResponse<T>(false, "Dado não encontrado.");
        }
    }
}
