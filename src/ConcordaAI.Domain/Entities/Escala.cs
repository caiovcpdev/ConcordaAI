using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Domain.Entities
{
    public class Escala
    {
        public Guid Id { get; private set; }
        public Guid EventoId { get; private set; }
        public string Nome { get; private set; }
        public DateTime Data { get; private set; }
        public TimeSpan HoraInicio { get; private set; }
        public TimeSpan HoraFim { get; private set; }
        public string PontoEncontro { get; private set; }
        public string? Observacoes { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public string CreatedBy { get; private set; }

        private readonly List<EscalaTrabalhador> _trabalhadores = new();
        public IReadOnlyCollection<EscalaTrabalhador> Trabalhadores => _trabalhadores.AsReadOnly();

        private Escala() { }

        public Escala(Guid eventoId,string nome, DateTime data, TimeSpan horaInicio, TimeSpan horaFim, string pontoEncontro, string createdBy, string? observacoes = null)
        {
            if (eventoId == Guid.Empty)
                throw new ArgumentException("EventoId inválido.");

            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome da escala é obrigatório.");

            if (horaFim <= horaInicio)
                throw new ArgumentException("Hora fim deve ser maior que hora início.");

            if (string.IsNullOrWhiteSpace(pontoEncontro))
                throw new ArgumentException("Ponto de encontro é obrigatório.");

            if (string.IsNullOrWhiteSpace(createdBy))
                throw new ArgumentException("CreatedBy é obrigatório.");

            Id = Guid.NewGuid();
            EventoId = eventoId;
            Nome = nome;
            Data = data.Date;
            HoraInicio = horaInicio;
            HoraFim = horaFim;
            PontoEncontro = pontoEncontro;
            Observacoes = observacoes;
            CreatedAt = DateTime.UtcNow;
            CreatedBy = createdBy;
        }

        public void AdicionarTrabalhador(Guid eventoTrabalhadorId)
        {
            if (eventoTrabalhadorId == Guid.Empty)
                throw new ArgumentException("Trabalhador inválido.");

            if (_trabalhadores.Any(t => t.EventoTrabalhadorId == eventoTrabalhadorId))
                throw new InvalidOperationException("Trabalhador já está vinculado a esta escala.");

            var escalaTrabalhador = new EscalaTrabalhador(
                Id,
                eventoTrabalhadorId,
                HoraInicio,
                HoraFim);

            _trabalhadores.Add(escalaTrabalhador);
        }

        public void RemoverTrabalhador(Guid eventoTrabalhadorId)
        {
            var trabalhador = _trabalhadores
                .FirstOrDefault(t => t.EventoTrabalhadorId == eventoTrabalhadorId);

            if (trabalhador is null)
                throw new InvalidOperationException("Trabalhador não está vinculado a esta escala.");

            _trabalhadores.Remove(trabalhador);
        }
    }
}
