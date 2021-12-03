using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Validators.Cliente
{
    internal class UpdateClienteValidator : ClienteValidator
    {
        public UpdateClienteValidator()
        {
            base.ValidateId();
            base.ValidateNome();
            base.ValidateEmail();
            base.ValidateTelefone();
        }
    }
}
