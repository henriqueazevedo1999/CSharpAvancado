using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Validators.Cliente
{
    public class InsertClienteValidator : ClienteValidator
    {
        public InsertClienteValidator()
        {
            base.ValidateNome();
            base.ValidateCPF();
            base.ValidateDataNascimento();  
            base.ValidateEmail();   
            base.ValidateTelefone();    
        }
    }
}
