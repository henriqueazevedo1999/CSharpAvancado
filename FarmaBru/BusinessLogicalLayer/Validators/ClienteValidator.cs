using FluentValidation;
using MetaData;
using BusinessLogicalLayer.Extensions;
using System;

namespace BusinessLogicalLayer.Validators
{
    internal class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome deve ser informado.")
                .Length(3, 70).WithMessage("Nome deve conter entre 3 e 70 caracteres");

            RuleFor(x => x.CPF).NotEmpty().WithMessage("CPF deve ser informado.")
                .Length(11).WithMessage("CPF deve conter 11 caracteres.")
                .Must(x => x.IsValidCPF()).WithMessage("CPF inválido.");

            RuleFor(x => x.Telefone).NotEmpty().WithMessage("Telefone deve ser informado.")
                .Length(9, 15).WithMessage("Telefone deve conter entre 9 e 15 caracteres.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email deve ser informado.")
                .Length(10, 100).WithMessage("Email deve conter entre 10 e 100 caracteres.")
                .EmailAddress().WithMessage("Email inválido.");

            RuleFor(x => x.DataNascimento).GreaterThan(DateTime.Now.AddYears(-110)).WithMessage("Data inválida.");
        }
    }
}
