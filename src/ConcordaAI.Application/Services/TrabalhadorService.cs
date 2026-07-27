using ConcordaAI.Application.Common;
using ConcordaAI.Application.DTOs.Eventos;
using ConcordaAI.Application.DTOs.Trabalhadores;
using ConcordaAI.Application.Interfaces;
using ConcordaAI.Domain.Entities;
using ConcordaAI.Domain.Interfaces;
using FluentValidation;
using System.Data;


namespace ConcordaAI.Application.Services
{
    public class TrabalhadorService : ITrabalhadorService
    {
        private readonly ITrabalhadorRepository _trabalhadorRepository;
        private readonly IValidator <CriarTrabalhadorRequest> _validator;

        public TrabalhadorService(ITrabalhadorRepository trabalhadorRepository, IValidator<CriarTrabalhadorRequest> validator)
        {
            _trabalhadorRepository = trabalhadorRepository;
            _validator = validator;
        }

        public async Task<Result> AlterarStatusAsync(Guid id, string novoStatus)
        {
            var trabalhador = await _trabalhadorRepository.ObterPorIdAsync(id);

            if (trabalhador is null)
                return Result.Fail("Trabalhador não encontrado.");

            switch (novoStatus.ToLower())
            {
                case "ativo":
                    trabalhador.Reativar();
                    break;

                case "inativo":
                    trabalhador.Inativar();
                    break;

                case "bloqueado":
                    trabalhador.Bloquear();
                    break;

                default:
                    return Result.Fail("Status inválido.");
            }

            await _trabalhadorRepository.AtualizarAsync(trabalhador);

            return Result.Ok();
        }

        public async Task<Result<TrabalhadorResponse>> CriarAsync(CriarTrabalhadorRequest request)
        {
            var validation = await _validator.ValidateAsync(request);

            if (!validation.IsValid)
                return Result<TrabalhadorResponse>.Fail(string.Join(" | ", validation.Errors.Select(x => x.ErrorMessage)));

            var existente = await _trabalhadorRepository.ObterPorCpfAsync(request.CPF);

            if (existente != null)
                return Result<TrabalhadorResponse>.Fail("CPF já cadastrado");

            var trabalhador = new Trabalhador(
                request.Nome,
                request.CPF,
                request.DataNascimento,
                request.Sexo,
                request.Telefone,
                request.Endereco,
                request.Cidade,
                request.Estado,
                request.CEP,
                request.CreatedBy
            );

            await _trabalhadorRepository.AdicionarAsync(trabalhador);

            return Result<TrabalhadorResponse>.Ok(Mapear(trabalhador));
        }

        public async Task<Result<TrabalhadorResponse>> ObterPorIdAsync(Guid id)
        {
            var trabalhador = await _trabalhadorRepository.ObterPorIdAsync(id);

            if (trabalhador is null)
                return Result<TrabalhadorResponse>.Fail("Trabalhador não encontrado.");

            return Result<TrabalhadorResponse>.Ok(Mapear(trabalhador));
        }

        public async Task<Result<IEnumerable<TrabalhadorResponse>>> ObterTodosAsync()
        {
            var trabalhadores = await _trabalhadorRepository.ObterTodosAsync();
            return Result<IEnumerable<TrabalhadorResponse>>.Ok(trabalhadores.Select(Mapear));
        }

        private static TrabalhadorResponse Mapear(Trabalhador trabalhador)
        {
            return new TrabalhadorResponse
            {
                Id = trabalhador.Id,
                Nome = trabalhador.Nome,
                CPF = trabalhador.CPF,
                Status = trabalhador.Status.ToString()
            };
        }
    }
}
