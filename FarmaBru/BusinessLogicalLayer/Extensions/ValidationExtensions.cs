using Common;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Extensions
{
    static class ValidationExtensions
    {
        public static Response ToResponse(this ValidationResult result)
        {
            if (result.IsValid)
            {
                return new Response
                {
                    HasSuccess = true,
                    Message = "Validado com sucesso"
                };
            }

            StringBuilder sb = new();

            foreach (ValidationFailure item in result.Errors)
            {
                sb.AppendLine(item.ErrorMessage);
            }

            return new Response
            {
                HasSuccess = false,
                Message = sb.ToString()
            };
        }
    }
}
