# BankSim - Sistema de Simulação Bancária

O **BankSim** é uma API desenvolvida em .NET 8 que simula as operações essenciais de um banco digital. O projeto foca em **Arquitetura Limpa (Clean Architecture)**, boas práticas de **POO** e integridade transacional.

---

## Funcionalidades

- **Gestão de Clientes:** Cadastro e consulta de clientes (validação de unicidade de CPF).
- **Gestão de Contas:**
  - Suporte a **Conta Corrente** (com Limite Especial).
  - Suporte a **Conta Poupança** (com rendimento mensal).
- **Operações Financeiras:**
  - **Depósito:** Crédito em conta.
  - **Saque:** Débito com validação de saldo e limite.
  - **Transferência:** Operação atômica entre contas com aplicação automática de taxas.
- **Histórico (Ledger):** Extrato completo e imutável de transações.
- **Segurança e Robustez:**
  - Middleware global para tratamento de erros padronizado.
  - Validações de regra de negócio na camada de serviço.

---

## 🛠️ Tecnologias Utilizadas

- **.NET 8 SDK** (C#)
- **ASP.NET Core Web API**
- **Entity Framework Core 8** (ORM)
- **SQL Server 2022** (Banco de Dados)
- **Docker** (Containerização do Banco)
- **SwaggerUI**

---

## Arquitetura

O projeto segue a **Clean Architecture**, dividido em camadas:

1.  **Domain:** Entidades (`Client`, `Account`, `Transaction`), Enums e Interfaces de Repositório.
2.  **Application:** Serviços (`Services`), Interfaces de Serviço, DTOs (`InputModels`, `ViewModels`) e Mappers. Contém toda a regra de negócio.
3.  **Infra:** Implementação do acesso a dados (`DbContext`, `Repositories`) e configurações do EF Core.
4.  **Api:** Controllers, Middlewares e Injeção de Dependência.

---

## Como Executar o Projeto

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- Visual Studio 2022 ou VS Code

### Passo 1: Clonar o Repositório
```bash
git clone https://github.com/bispomariana/BankSim.git
cd BankSim
```

### Passo 2: Subir o Banco de Dados (Docker)

Execute o comando abaixo para criar o container do SQL Server:
```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=SenhaSuperSegura123" -p 1434:1433 --name sqlserver_banksim -d [mcr.microsoft.com/mssql/server:2022-latest](https://mcr.microsoft.com/mssql/server:2022-latest)
```
### Passo 3: Configurar a Connection String

Verifique se o arquivo appsettings.json na pasta ProjetoDevTrail.Api está configurado corretamente:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=BankSim;User Id=sa;Password=SenhaSuperSegura123;TrustServerCertificate=True;"
}
```

### Passo 4: Criar o Banco de Dados (Migrations)

Na raiz da solução, execute as migrations para criar as tabelas:
```bash
dotnet ef database update --project ProjetoDevTrail.Infra --startup-project ProjetoDevTrail
```

### Passo 5: Rodar a API

```bash

dotnet run --project ProjetoDevTrail
```

Acesse o Swagger em: https://localhost:5082/swagger (ou a porta indicada no terminal).

---

## Licença

Este projeto está sob a licença **MIT**. Isso significa que você pode utilizá-lo, estudá-lo e modificá-lo livremente. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---

## Autor

Desenvolvido por Mariana Bispo como desafio final de uma trilha focada em Arquitetura de Software e .NET.

[![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/mariana-bispo-840653263/) 
[![GitHub](https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white)](https://github.com/bispomariana)
