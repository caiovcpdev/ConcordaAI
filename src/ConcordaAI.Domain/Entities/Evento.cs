using ConcordaAI.Domain.Enums;

namespace ConcordaAI.Domain.Entidades
{
    public class Evento
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataFim { get; private set; }
        public EventoStatus Status { get; private set; }
        public string Organizador { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string CreatedBy { get; private set; }

        //para o dapper
        private Evento() { }

        public Evento(string nome, string cidade, string estado, DateTime dataInicio, DateTime dataFim, string organizador, string createdBy)
        {
            Validar(nome, organizador, dataInicio, dataFim);

            Id = Guid.NewGuid();
            Nome = nome;
            Cidade = cidade;
            Estado = estado;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Organizador = organizador;
            Status = EventoStatus.Planejado;
            CreatedAt = DateTime.UtcNow;
            CreatedBy = createdBy;


        }

        public void Ativar()
        {
            if (Status != EventoStatus.Planejado)
                throw new InvalidOperationException("Somente eventos planejados podem ser ativados.");

            Status = EventoStatus.Ativo;
        }

        public void Encerrar()
        {
            if (Status != EventoStatus.Ativo)
                throw new InvalidOperationException("Somente eventos ativos podem ser encerrados.");

            Status = EventoStatus.Encerrado;
        }

        public void Cancelar()
        {
            if (Status == EventoStatus.Encerrado)
                throw new InvalidOperationException("Eventos encerrados não podem ser cancelados.");

            Status = EventoStatus.Cancelado;
        }
        private void Validar(string nome, string organizador, DateTime inicio, DateTime fim)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome do evento é obrigatório.");

            if (string.IsNullOrWhiteSpace(organizador))
                throw new ArgumentException("Organizador é obrigatório.");

            if (fim < inicio)
                throw new ArgumentException("Data fim não pode ser menor que data início.");
        }
    }
}
