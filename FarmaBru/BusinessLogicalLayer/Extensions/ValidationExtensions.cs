using Common;
using FluentValidation.Results;
using System.Text;

namespace BusinessLogicalLayer.Extensions
{
    static class ValidationExtensions
    {
        public static Response ToResponse(this ValidationResult result)
        {
            if (result.IsValid)
            {
                return new Response(true, "Validado com sucesso");
            }

            var sb = new StringBuilder();

            foreach (ValidationFailure item in result.Errors)
            {
                sb.AppendLine(item.ErrorMessage);
            }

            return new Response(false, sb.ToString());
        }
    }
}
