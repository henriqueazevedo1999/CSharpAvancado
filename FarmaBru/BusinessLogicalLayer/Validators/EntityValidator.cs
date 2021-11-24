using FluentValidation;
using MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Validators
{
    internal class EntityValidator<T> : AbstractValidator<T> where T : Entity
    {
        public void ValidateId()
        {
            RuleFor(x => x.ID).GreaterThan(0).WithMessage("ID não informado.");
        }
    }
}
