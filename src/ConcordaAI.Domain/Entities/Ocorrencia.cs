using ConcordaAI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Domain.Entities
{
    public class Ocorrencia
    {
        public Guid Id { get; private set; }
        public Guid EventoTrabalhadorId { get; private set; }

        public TipoOcorrencia Tipo { get; private set; }
        public GravidadeOcorrencia Gravidade { get; private set; }

        public string Descricao { get; private set; }
        public DateTime DataHora { get; private set; }

        public bool Resolvida { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public string CreatedBy { get; private set; }

        private Ocorrencia() { }

        public Ocorrencia(Guid eventoTrabalhadorId, TipoOcorrencia tipo, GravidadeOcorrencia gravidade, string descricao, string createdBy)
        {
            if (eventoTrabalhadorId == Guid.Empty) 
                throw new ArgumentException("EventoTrabalhadorId inválido.");
            
            if (string.IsNullOrEmpty(descricao))
                throw new ArgumentException("Descrição obrigatória.");

            if (string.IsNullOrEmpty(createdBy))
                throw new ArgumentException("CreatedBy obrigatório.");

            Id = Guid.NewGuid();
            EventoTrabalhadorId = eventoTrabalhadorId; 
            Tipo = tipo;
            Gravidade = gravidade; 
            Descricao = descricao.Trim();
            DataHora = DateTime.Now;
            Resolvida = false;
        }

        public void MarcarComoResolvida()
        {
            if (Resolvida)
                throw new InvalidOperationException("A ocorrência já está resolvida.");

            Resolvida = true;
        }

        public void AtualizarDescricao(string novaDescricao)
        {
            if (string.IsNullOrEmpty(novaDescricao))
                throw new ArgumentException("Descrição inválida.");

            Descricao = novaDescricao.Trim();
        }
    }
}
