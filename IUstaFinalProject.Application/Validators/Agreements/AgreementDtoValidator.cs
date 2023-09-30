using FluentValidation;
using IUstaFinalProject.Domain.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Validators.Agreements
{
    public class AgreementDtoValidator:AbstractValidator<AgreementDto>
    {
        public AgreementDtoValidator() {
            RuleFor(p => p.CustomerId)
                .NotEmpty()
                .NotNull()
                .WithMessage("Do not leave empty");
            RuleFor(p => p.WorkerId)
                .NotEmpty()
                .NotNull()
                .WithMessage("Do not leave empty");
            RuleFor(p => p.AgreementText)
                .NotEmpty()
                .NotNull()
                .WithMessage("Do not leave empty");
        }
    }
}
