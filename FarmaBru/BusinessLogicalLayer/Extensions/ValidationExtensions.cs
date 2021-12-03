using Common.Response;
using FluentValidation.Results;
using System.Text;

namespace BusinessLogicalLayer.Extensions
{
    static class ValidationExtensions
    {
        public static BaseResponse ToResponse(this ValidationResult result)
        {
            if (result.IsValid)
            {
                return new BaseResponse(true, "Validado com sucesso");
            }

            var sb = new StringBuilder();

            foreach (ValidationFailure item in result.Errors)
            {
                sb.AppendLine(item.ErrorMessage);
            }

            return new BaseResponse(false, sb.ToString());
        }
    }
}
