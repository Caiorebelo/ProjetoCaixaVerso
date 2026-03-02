# CaixaVerso - API de Simulação de Investimentos

Este projeto foi desenvolvido como parte do **Desafio Técnico Back-end - CaixaVerso**.  
O objetivo é construir uma API que simula investimentos com base em produtos parametrizados em banco de dados, persistindo o histórico de simulações.

---

## 🚀 Tecnologias Utilizadas
- .NET 9 (ASP.NET Web API)
- Entity Framework Core
- SQLite
- xUnit + Moq (testes automatizados)
- Swagger (documentação dos endpoints)

---

## 📂 Estrutura do Projeto
CaixaVerso/
├── Domain/Entities        # Entidades (Produto, Simulacao)
├── Domain/Interfaces      # Interfaces de repositórios
├── Application/UseCases   # Casos de uso (CriarSimulacao, etc.)
├── Infrastructure/Data    # DbContext e Seed
├── Controllers            # Endpoints REST
└── Tests                  # Testes automatizados

---

## ⚙️ Configuração

### 1. Clonar o repositório
git clone https://github.com/seuusuario/CaixaVerso.git
cd CaixaVerso

2. Restaurar dependências
dotnet restore

3. Rodar a aplicação
dotnet run --project CaixaVerso


A API estará disponível em:
http://localhost:5000

Swagger:
http://localhost:5000/swagger


📊 Endpoints
Criar Simulação
POST /simulacoes

Request:
{
  "clienteId": 123,
  "valor": 10000.00,
  "prazoMeses": 12,
  "tipoProduto": "CDB"
}

Response:
{
  "produtoValidado": {
    "id": 1,
    "nome": "CDB Caixa 2026",
    "tipo": "CDB",
    "rentabilidade": 0.12,
    "risco": "Baixo"
  },
  "resultadoSimulacao": {
    "valorFinal": 11200.00,
    "prazoMeses": 12
  },
  "dataSimulacao": "2026-03-02T14:00:00Z"
}

Histórico de Simulações
GET /simulacoes?clienteId=123

Response:
[
  {
    "id": 1,
    "clienteId": 123,
    "produto": "CDB Caixa 2026",
    "valorInvestido": 10000.00,
    "valorFinal": 11200.00,
    "prazoMeses": 12,
    "dataSimulacao": "2026-03-02T14:00:00Z"
  }
]


🧪 Testes Automatizados
Rodar testes
dotnet test

Cobertura de código
dotnet test --collect:"XPlat Code Coverage"


Testes implementados:

✅ Cálculo da simulação

✅ POST /simulacoes (sucesso e erro)

✅ GET /simulacoes (histórico)


🐳 Docker
Build da imagem
docker build -t caixa-verso .

Rodar container
docker run -p 5000:5000 caixa-verso


🏆 Critérios atendidos
Funcionamento da API

Organização e clareza do código

Validações e tratamento de erros

Testes automatizados (80%+ cobertura)

Documentação completa

📌 Observações
O projeto foi desenvolvido seguindo princípios SOLID e boas práticas de arquitetura, garantindo escalabilidade e manutenção futura.