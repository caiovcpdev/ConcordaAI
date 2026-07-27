using ConcordaAI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Domain.Entities
{
    public class Trabalhador
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string? RG { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public int Sexo { get; private set; }
        public string Telefone { get; private set; }
        public string? Email { get; private set; }
        public string Endereco { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string CEP { get; private set; }
        public string? Foto { get; private set; }
        public string? Pix { get; private set; }
        public string? Banco { get; private set; }
        public string? Agencia { get; private set; }
        public string? Conta { get; private set; }
        public TrabalhadorStatus Status { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public string CreatedBy { get; private set; }

        private Trabalhador() { }

        public Trabalhador(
        string nome,
        string cpf,
        DateTime dataNascimento,
        int sexo,
        string telefone,
        string endereco,
        string cidade,
        string estado,
        string cep,
        string createdBy,
        string? rg = null,
        string? email = null)
        {
            Validar(nome, cpf, dataNascimento);

            Id = Guid.NewGuid();
            Nome = nome;
            CPF = LimparCpf(cpf);
            DataNascimento = dataNascimento;
            Sexo = sexo;
            Telefone = telefone;
            Endereco = endereco;
            Cidade = cidade;
            Estado = estado;
            CEP = cep;
            RG = rg;
            Email = email;

            Status = TrabalhadorStatus.Ativo;
            CreatedAt = DateTime.UtcNow;
            CreatedBy = createdBy;
        }
        public void Inativar()
        {
            if (Status == TrabalhadorStatus.Inativo)
                throw new InvalidOperationException("Trabalhador já está inativo.");

            Status = TrabalhadorStatus.Inativo;
        }

        public void Bloquear()
        {
            if (Status == TrabalhadorStatus.Bloqueado)
                throw new InvalidOperationException("Trabalhador já está bloqueado.");

            Status = TrabalhadorStatus.Bloqueado;
        }

        public void Reativar()
        {
            if (Status == TrabalhadorStatus.Ativo)
                throw new InvalidOperationException("Trabalhador já está ativo.");

            Status = TrabalhadorStatus.Ativo;
        }

        private void Validar(string nome, string cpf, DateTime dataNascimento)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException("CPF é obrigatório.");

            var cpfLimpo = LimparCpf(cpf);

            if (cpfLimpo.Length != 11)
                throw new ArgumentException("CPF deve conter 11 dígitos.");

            if (dataNascimento > DateTime.UtcNow.Date)
                throw new ArgumentException("Data de nascimento inválida.");
        }

        private string LimparCpf(string cpf)
        {
            return new string(cpf.Where(char.IsDigit).ToArray());
        }
    }
}
