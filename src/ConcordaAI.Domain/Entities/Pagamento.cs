using ConcordaAI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Domain.Entities
{
    public class Pagamento
    {
        public Guid Id { get; private set; }
        public Guid EventoTrabalhadorId { get; private set; }

        public decimal ValorPrevisto { get; private set; }
        public decimal? ValorPago { get; private set; }

        public DateTime DataPrevista { get; private set; }
        public DateTime? DataPagamento { get; private set; }

        public FormaPagamento FormaPagamento { get; private set; }
        public StatusPagamento Status { get; private set; }

        public string? ComprovanteUrl { get; private set; }
        public string? Observacoes { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public string CreatedBy { get; private set; }

        private Pagamento() { }

        public Pagamento(Guid eventoTrabalhadorId, decimal valorPrevisto, DateTime dataPrevista, FormaPagamento formaPagamento, string createdBy, string? observacoes = null)
        {
            if (eventoTrabalhadorId == Guid.Empty)
                throw new ArgumentException("EventoTrabalhador inválido.");
            
            if (valorPrevisto <= 0)
                throw new ArgumentException("Valor previsto deve ser maior que zero.");
            
            if (string.IsNullOrEmpty(createdBy))
                throw new ArgumentException("CreatedBy é obrigatório.");

            Id = Guid.NewGuid();
            EventoTrabalhadorId = eventoTrabalhadorId;
            ValorPrevisto = valorPrevisto;  
            DataPrevista = dataPrevista.Date;
            FormaPagamento = formaPagamento;
            Observacoes = observacoes;

            Status = StatusPagamento.Pendente;
            CreatedAt = DateTime.UtcNow;
            CreatedBy = createdBy;
        }

        public void Aprovar() 
        {
            if (Status != StatusPagamento.Pendente)
                throw new InvalidOperationException("Somente pagamentos pendentes podem ser aprovados.");

            Status = StatusPagamento.Aprovado;
        }

        public void Rejeitar(string motivo)
        {
            if (Status == StatusPagamento.Pago)
                throw new InvalidOperationException("Pagamento já foi realizado.");

            if (string.IsNullOrEmpty(motivo))
                throw new ArgumentException("Motivo da rejeição é obrigatório.");

            Observacoes = motivo;
            Status = StatusPagamento.Rejeitado;
        }

        public void Cancelar ()
        {
            if(Status == StatusPagamento.Pago)
                throw new InvalidOperationException("Pagamento já foi realizado.");

            Status = StatusPagamento.Cancelado;
        }

        public void RegistrarPagamento(decimal valorPago, FormaPagamento formaPagamento, string? comprovanteUrl)
        {
            if (Status != StatusPagamento.Aprovado)
                throw new InvalidOperationException("Pagamento deve estar aprovado para ser pago.");

            if (valorPago <= 0)
                throw new ArgumentException("Valor pago deve ser maior que zero.");

            ValorPago = valorPago;
            FormaPagamento = formaPagamento;
            DataPagamento = DateTime.UtcNow;
            ComprovanteUrl = comprovanteUrl;
            Status = StatusPagamento.Pago;
        }

        public void AtualizarObservacoes(string? observacoes)
        {
            Observacoes = observacoes;
        }
    }
}
