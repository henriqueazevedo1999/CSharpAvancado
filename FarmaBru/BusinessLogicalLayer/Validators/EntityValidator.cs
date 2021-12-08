﻿using FluentValidation;
using MetaData.Entities;

namespace BusinessLogicalLayer.Validators
{
    public class EntityValidator<T> : AbstractValidator<T> where T : Entity
    {
        public void ValidateId()
        {
            RuleFor(x => x.ID).GreaterThan(0).WithMessage("ID não informado.");
        }
    }
}
