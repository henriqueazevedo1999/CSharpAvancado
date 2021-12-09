using FluentValidation;
using BusinessLogicalLayer.Extensions;
using API.Application.Commands;

namespace API.Application.Validators
{
    public class ExcluiValidator : AbstractValidator<ExcluiCommand>
    {
        public ExcluiValidator()
        {
            this.ValidateId();
        }

        public void ValidateId()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("ID não informado.");
        }
    }
}
