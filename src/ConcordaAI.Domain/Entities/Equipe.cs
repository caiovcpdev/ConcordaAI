using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Domain.Entities
{
    public class Equipe
    {
        public Guid Id { get; private set; }
        public Guid EventoId { get; private set; }
        public string Nome { get; private set; }
        public Guid LiderEventoTrabalhadorId { get; private set; }
        public int QuantidadeMaxima { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public string CreatedBy { get; private set; }

        private readonly List<Guid> _membros = new();
        public IReadOnlyCollection<Guid> Membros => _membros.AsReadOnly();

        private Equipe() { }

        public Equipe (Guid eventoId, string nome,Guid liderEventoTrabalhadorId,int quantidadeMaxima, string createdBy)
        {
            if (eventoId == Guid.Empty)
                throw new ArgumentException("EventoId inválido.");

            if (liderEventoTrabalhadorId == Guid.Empty)
                throw new ArgumentException("Líder inválido.");
            
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException("Nome da equipe é obrigatório.");

            if (string.IsNullOrEmpty(createdBy))
                throw new ArgumentException("Created é obrigatório.");
            
            if (quantidadeMaxima <= 0)
                throw new ArgumentException("Quantidde máxima deve ser maior do que zero.");

            Id = Guid.NewGuid();
            EventoId = eventoId;
            Nome =  nome;
            LiderEventoTrabalhadorId = liderEventoTrabalhadorId;
            QuantidadeMaxima = quantidadeMaxima;
            CreatedAt = DateTime.UtcNow;
            CreatedBy = createdBy;
        }

        public void AdicionarMembro(Guid eventoTrabalhadorId)
        {
            if (eventoTrabalhadorId == Guid.Empty)
                throw new ArgumentException("Membro inválido.");

            if (_membros.Contains(eventoTrabalhadorId))
                throw new InvalidOperationException("Trabalhador já pertence a esta equipe.");

            if (_membros.Count >= QuantidadeMaxima)
                throw new InvalidOperationException("Equipe já atingiu a quantidade máxima.");

            _membros.Add(eventoTrabalhadorId);
        }


        public void RemoverMembrol(Guid eventoTrabalhadorId)
        {
            if (!_membros.Contains(eventoTrabalhadorId))
                throw new InvalidOperationException("Trabalhador não pertence a esta equipe.");

            _membros.Remove(eventoTrabalhadorId);
        } 

        public void AlterarLider(Guid novoLiderEventoTrabalhadorId)
        {
            if (novoLiderEventoTrabalhadorId == Guid.Empty)
                throw new ArgumentException("Novo líder inválido.");

            LiderEventoTrabalhadorId = novoLiderEventoTrabalhadorId;
            
        }
    }
}
