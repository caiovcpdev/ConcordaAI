using ConcordaAI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Domain.Entities
{
    public class EventoTrabalhador
    {
        public Guid Id { get; private set; }
        public Guid EventoId { get; private set; }
        public Guid TrabalhadorId { get; private set; }

        public TipoTrabalhador TipoTrabalhador { get; private set; }
        public EventoTrabalhadorStatus Status { get; private set; }

        public decimal? ValorDiaria { get; private set; }
        public string? Observacoes { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public string CreatedBy { get; private set; }

        private EventoTrabalhador() { }

        public EventoTrabalhador(Guid eventoId, Guid trabalhadorId, TipoTrabalhador tipoTrabalhador, string createdBy, decimal? valorDiaria = null, string? observacoes = null)
        {
            if (eventoId == Guid.Empty)
                throw new ArgumentException("EventoId inválido.");

            if (trabalhadorId == Guid.Empty)
                throw new ArgumentException("TrabalhadorId inválido.");

            if (string.IsNullOrWhiteSpace(createdBy))
                throw new ArgumentException("CreatedBy é obrigatório.");

            Id = Guid.NewGuid();
            EventoId = eventoId;
            TrabalhadorId = trabalhadorId;
            TipoTrabalhador = tipoTrabalhador;
            ValorDiaria = valorDiaria;
            Observacoes = observacoes;

            Status = EventoTrabalhadorStatus.Vinculado;
            CreatedAt = DateTime.UtcNow;
            CreatedBy = createdBy;
        }

        public void Confirmar()
        {
            if (Status != EventoTrabalhadorStatus.Vinculado)
                throw new InvalidOperationException("Somente vínculos vinculados podem ser confirmados.");

            Status = EventoTrabalhadorStatus.Confirmado;
        }

        public void Cancelar()
        {
            if (Status == EventoTrabalhadorStatus.Finalizado)
                throw new InvalidOperationException("Não é possível cancelar vínculo finalizado.");

            Status = EventoTrabalhadorStatus.Cancelado;
        }

        public void Substituir()
        {
            if (Status == EventoTrabalhadorStatus.Finalizado)
                throw new InvalidOperationException("Não é possível substituir vínculo finalizado.");

            Status = EventoTrabalhadorStatus.Substituido;
        }

        public void Finalizar()
        {
            if (Status != EventoTrabalhadorStatus.Confirmado)
                throw new InvalidOperationException("Somente vínculos confirmados podem ser finalizados.");

            Status = EventoTrabalhadorStatus.Finalizado;
        }

        public void DefinirValorDiaria(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("Valor da diária deve ser maior que zero.");

            ValorDiaria = valor;
        }

        public void AtualizarObservacoes(string? observacoes)
        {
            Observacoes = observacoes;
        }
    }
}