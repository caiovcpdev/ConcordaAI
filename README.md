# ConcordaAI

## Plataforma de Gestão Operacional para Grandes Eventos

---

## 📋 Visão Geral

O **ConcordaAI** é uma plataforma completa para gestão operacional de grandes eventos como Carnaval, Micaretas, São João, Festivais e Eventos Esportivos.

O sistema permite:

- Gestão de eventos
- Cadastro e controle de trabalhadores (cordeiros, ambulantes, etc.)
- Vínculo de trabalhadores a eventos
- Organização de escalas por turno
- Registro de check-in e check-out com geolocalização
- Registro de ocorrências disciplinares
- Gestão de pagamentos
- Controle de usuários com perfis de acesso
- Autenticação segura via JWT

---

## 🛠️ Tecnologias

### Backend

| Tecnologia | Versão |
|---|---|
| .NET | 9 |
| ASP.NET Core Web API | 9 |
| SQL Server | 2022+ |
| Dapper | Latest |
| JWT Bearer | Latest |
| FluentValidation | Latest |
| Serilog | Latest |
| BCrypt.Net | Latest |
| xUnit | Latest |

### Frontend

| Tecnologia | Versão |
|---|---|
| React | 18+ |
| Vite | Latest |
| TypeScript | 5+ |
| TailwindCSS | 4+ |
| Axios | Latest |
| React Router | 6+ |

---

## 🏗️ Arquitetura

O backend segue **Clean Architecture** com princípios de **DDD (Domain-Driven Design)**.


src/
├── ConcordaAI.Api → Camada de apresentação (Controllers, Middlewares)
├── ConcordaAI.Application → Casos de uso (Services, DTOs, Validators)
├── ConcordaAI.Domain → Entidades, Enums, Interfaces de repositório
├── ConcordaAI.Infrastructure → Implementações (Dapper, SQL Server, JWT)
└── ConcordaAI.Shared → Cross-cutting concerns



### Princípios aplicados

- SOLID
- Clean Code
- Repository Pattern
- Result Pattern
- Dependency Injection
- Middleware global de exceções
- Role-based Authorization

---

## 🚀 Pré-requisitos

- .NET 9 SDK
- SQL Server 2022 ou superior
- Node.js 18+
- npm ou yarn

---

## ⚙️ Como Rodar o Backend

### 1. Clonar o repositório

```bash
git clone https://github.com/seu-usuario/concordaai.git
cd concordaai

2. Criar o banco de dados
Execute o script SQL de criação do banco no SQL Server Management Studio ou Azure Data Studio.

O script está em:

text

docs/sql/criar-banco.sql
3. Configurar a connection string
Edite o arquivo src/ConcordaAI.Api/appsettings.json:

JSON

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ConcordaAI;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "SUA_CHAVE_SECRETA_DE_32_CARACTERES_MINIMO",
    "Issuer": "ConcordaAI",
    "Audience": "ConcordaAI",
    "ExpireMinutes": "120"
  }
}
4. Criar usuário administrador
Execute no banco:

SQL

INSERT INTO Usuario (Id, Nome, Email, SenhaHash, Perfil, Ativo, CreatedAt)
VALUES (
    NEWID(),
    'Administrador',
    'admin@concordaai.com',
    '$2a$11$HASH_BCRYPT_AQUI',
    1,
    1,
    SYSUTCDATETIME()
);
5. Rodar a aplicação
Bash

dotnet run --project src/ConcordaAI.Api
A API estará disponível em:

text

https://localhost:5001

🔐 Autenticação
O sistema utiliza JWT (JSON Web Token).

Login
text

POST /api/v1/auth/login
JSON

{
  "email": "admin@concordaai.com",
  "senha": "Admin@123"
}
Resposta
JSON

{
  "success": true,
  "data": {
    "token": "eyJhbGciOi...",
    "expiracao": "2026-07-28T12:00:00Z"
  },
  "errors": null
}
Perfis de acesso
Perfil	Código	Acesso
Administrador	1	Total
Supervisor	2	Gerencial
Líder	3	Operacional
Operador	4	Execução
Trabalhador	5	Próprios dados
📡 Endpoints da API
Auth
Método	Rota	Descrição
POST	/api/v1/auth/login	Login
POST	/api/v1/auth/usuarios	Criar usuário (Admin)
Eventos
Método	Rota	Descrição
GET	/api/v1/eventos	Listar eventos
POST	/api/v1/eventos	Criar evento
GET	/api/v1/eventos/{id}	Buscar evento
Trabalhadores
Método	Rota	Descrição
GET	/api/v1/trabalhadores	Listar trabalhadores
POST	/api/v1/trabalhadores	Criar trabalhador
GET	/api/v1/trabalhadores/{id}	Buscar trabalhador
PATCH	/api/v1/trabalhadores/{id}/status	Alterar status
Evento Trabalhador
Método	Rota	Descrição
POST	/api/v1/eventos/{eventoId}/trabalhadores	Vincular trabalhador
GET	/api/v1/eventos/{eventoId}/trabalhadores	Listar vínculos
PATCH	/api/v1/eventos/trabalhadores/{id}/confirmar	Confirmar vínculo
PATCH	/api/v1/eventos/trabalhadores/{id}/cancelar	Cancelar vínculo
Escalas
Método	Rota	Descrição
POST	/api/v1/eventos/{eventoId}/escalas	Criar escala
GET	/api/v1/eventos/{eventoId}/escalas	Listar escalas
POST	/api/v1/escalas/{escalaId}/trabalhadores	Adicionar trabalhador
Check-in
Método	Rota	Descrição
POST	/api/v1/escalas/{escalaTrabalhadorId}/checkin	Check-in
POST	/api/v1/escalas/{escalaTrabalhadorId}/checkout	Check-out
GET	/api/v1/escalas/{escalaTrabalhadorId}/presenca	Ver presença
Ocorrências
Método	Rota	Descrição
POST	/api/v1/eventos/trabalhadores/{id}/ocorrencias	Registrar
GET	/api/v1/eventos/trabalhadores/{id}/ocorrencias	Listar
PATCH	/api/v1/ocorrencias/{id}/resolver	Resolver
Pagamentos
Método	Rota	Descrição
POST	/api/v1/eventos/trabalhadores/{id}/pagamentos	Criar
GET	/api/v1/eventos/trabalhadores/{id}/pagamentos	Listar
PATCH	/api/v1/pagamentos/{id}/aprovar	Aprovar
POST	/api/v1/pagamentos/{id}/pagar	Pagar
PATCH	/api/v1/pagamentos/{id}/cancelar	Cancelar
Health Check
Método	Rota	Descrição
GET	/health	Status do sistema
📊 Padrão de Resposta
Todas as respostas da API seguem o formato:

Sucesso
JSON

{
  "success": true,
  "data": {},
  "errors": null
}
Erro
JSON

{
  "success": false,
  "data": null,
  "errors": ["Mensagem de erro"]
}
🗄️ Banco de Dados
O sistema utiliza SQL Server com as seguintes tabelas:

Evento
Trabalhador
Empresa
EventoTrabalhador
Equipe
EquipeMembro
Escala
EscalaTrabalhador
CheckInEscala
Ocorrencia
Pagamento
Usuario
O script completo de criação está em docs/sql/criar-banco.sql.


