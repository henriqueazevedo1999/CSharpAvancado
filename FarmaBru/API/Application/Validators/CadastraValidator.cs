using FluentValidation;
using BusinessLogicalLayer.Extensions;
using API.Application.Commands;

namespace API.Application.Validators
{
    public class CadastraValidator : AbstractValidator<CadastraCommand>
    {
        public CadastraValidator()
        {
            this.ValidateNome();
            this.ValidateCPF();
            this.ValidateTelefone();
            this.ValidateEmail();
            this.ValidateDataNascimento();
        }

        private void ValidateNome()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome deve ser informado.")
                .Length(3, 70).WithMessage("Nome deve conter entre 3 e 70 caracteres");
        }

        private void ValidateCPF()
        {
            RuleFor(x => x.CPF).NotEmpty().WithMessage("CPF deve ser informado.")
                .Must(x => x.IsValidCPF()).WithMessage("CPF inválido.");
        }

        private void ValidateTelefone()
        {
            RuleFor(x => x.Telefone).NotEmpty().WithMessage("Telefone deve ser informado.")
                .Length(9, 15).WithMessage("Telefone deve conter entre 9 e 15 caracteres.");
        }

        private void ValidateEmail()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email deve ser informado.")
                .Length(10, 100).WithMessage("Email deve conter entre 10 e 100 caracteres.")
                .EmailAddress().WithMessage("Email inválido.");
        }

        private void ValidateDataNascimento()
        { 
            RuleFor(x => x.DataNascimento).GreaterThan(DateTime.Now.AddYears(-110)).WithMessage("Data inválida.");
        }
    }
}
