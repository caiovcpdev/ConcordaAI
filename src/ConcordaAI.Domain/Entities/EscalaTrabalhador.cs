using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Domain.Entities
{
    public class EscalaTrabalhador
    {
        public Guid Id { get; private set; }
        public Guid EscalaId { get; private set; }
        public Guid EventoTrabalhadorId { get; private set; }

        public TimeSpan HoraInicio { get; private set; }
        public TimeSpan HoraFim { get; private set; }
        public string? PontoEncontro { get; private set; }
        public string? Observacoes { get; private set; }
        public bool Confirmado {  get; private set; }

        public DateTime CreatedAt { get; private set; }

        private EscalaTrabalhador() { }

        internal EscalaTrabalhador (Guid escalaId, Guid eventoTrabalhadorId, TimeSpan horaInicio, TimeSpan horaFim)  //Evita criar vínculo fora da Escala.
        {
            Id = Guid.NewGuid();
            EscalaId = escalaId;   
            EventoTrabalhadorId = eventoTrabalhadorId;
            HoraInicio = horaInicio;
            HoraFim = horaFim;
            Confirmado = false;
            CreatedAt = DateTime.UtcNow;
        }

        public void Confirmar() 
        {
            Confirmado = true;
        }

        public void PersonalizarHorario(TimeSpan horaInicio, TimeSpan horaFim)
        {
            if (horaFim <= horaInicio)
                throw new ArgumentException("Hora fim deve ser maior que a hora inicio.");

            HoraInicio = horaInicio;
            HoraFim = horaFim;
        }

        public void DefinirPontoEncontro(string ponto)
        {
            if (string.IsNullOrEmpty(ponto)) 
                throw new ArgumentException("Ponto de encontro inválido.");

            PontoEncontro = ponto;
        }

        public void AtualizarObservacoes(string? observacoes)
        {
            Observacoes = observacoes;
        }
    }
}
