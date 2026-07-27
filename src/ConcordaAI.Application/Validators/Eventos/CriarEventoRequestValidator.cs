using ConcordaAI.Application.DTOs.Eventos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Application.Validators.Eventos
{
    public class CriarEventoRequestValidator : AbstractValidator<CriarEventoRequest>
    {
        public CriarEventoRequestValidator()
        {
            RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório.")
            .MaximumLength(200);

            RuleFor(x => x.Cidade)
                .NotEmpty().WithMessage("Cidade é obrigatória.")
                .MaximumLength(150);

            RuleFor(x => x.Estado)
                .NotEmpty().WithMessage("Estado é obrigatório.")
                .Length(2);

            RuleFor(x => x.Organizador)
                .NotEmpty().WithMessage("Organizador é obrigatório.")
                .MaximumLength(200);

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("CreatedBy é obrigatório.");

            RuleFor(x => x.DataInicio)
                .NotEmpty().WithMessage("Data início é obrigatória.");

            RuleFor(x => x.DataFim)
                .NotEmpty().WithMessage("Data fim é obrigatória.")
                .GreaterThanOrEqualTo(x => x.DataInicio)
                .WithMessage("Data fim não pode ser menor que data início.");
        }
    }
}
