using ClienteAPI.Application.Queries;
using FluentValidation;

namespace API.Application.Validators;

public class GetByIdValidator : AbstractValidator<GetByIdQuery>
{
    public GetByIdValidator()
    {
        this.ValidateId();
    }

    public void ValidateId()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("ID informado inválido")
            .GreaterThan(0).WithMessage("ID deve ser maior que zero.");
    }
}
