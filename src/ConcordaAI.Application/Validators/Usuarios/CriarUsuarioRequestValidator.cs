using ConcordaAI.Application.DTOs.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Application.Validators.Usuarios
{
    public class CriarUsuarioRequestValidator : AbstractValidator<CriarUsuarioRequest>
    {
        public CriarUsuarioRequestValidator()
        {
            RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email é obrigatório.")
                .EmailAddress().WithMessage("Email inválido.");

            RuleFor(x => x.Senha)
                .NotEmpty().WithMessage("Senha é obrigatória.")
                .MinimumLength(6).WithMessage("Senha deve ter no mínimo 6 caracteres.");

            RuleFor(x => x.Perfil)
                .InclusiveBetween(1, 5)
                .WithMessage("Perfil inválido.");
        }
    }
}
