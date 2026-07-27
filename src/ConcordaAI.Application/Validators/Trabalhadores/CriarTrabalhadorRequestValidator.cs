using ConcordaAI.Application.DTOs.Eventos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Application.Validators.Trabalhadores
{
    public class CriarTrabalhadorRequestValidator : AbstractValidator<CriarTrabalhadorRequest>
    {
        public CriarTrabalhadorRequestValidator()
        {
            RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório.");

            RuleFor(x => x.CPF)
                .NotEmpty().WithMessage("CPF é obrigatório.")
                .Length(11).WithMessage("CPF deve conter 11 dígitos.");

            RuleFor(x => x.Telefone)
                .NotEmpty().WithMessage("Telefone é obrigatório.");

            RuleFor(x => x.Endereco)
                .NotEmpty().WithMessage("Endereço é obrigatório.");

            RuleFor(x => x.Cidade)
                .NotEmpty().WithMessage("Cidade é obrigatória.");

            RuleFor(x => x.Estado)
                .NotEmpty().WithMessage("Estado é obrigatório.")
                .Length(2);

            RuleFor(x => x.CEP)
                .NotEmpty().WithMessage("CEP é obrigatório.");
        }
    }
}
