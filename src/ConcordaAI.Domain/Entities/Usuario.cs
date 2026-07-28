using ConcordaAI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string SenhaHash { get; private set; }
        public PerfilUsuario Perfil { get; private set; }
        public bool Ativo { get; private set; }

        public DateTime CreatedAt { get; private set; }

        private Usuario () { }

        public Usuario(string nome, string email, string senhaHash, PerfilUsuario perfil)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentNullException("Nome é obrigatório.");

            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("Email é obrigatório.");

            if (string.IsNullOrEmpty(senhaHash))
                throw new ArgumentNullException("Senha inválida.");

            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;  
            SenhaHash = senhaHash;
            Perfil = perfil;
            Ativo = true;
        }

        public void Desativar()
        {
            Ativo = false;
        }
        
        public void Ativar()
        {
            Ativo = true;
        }
    }
}
