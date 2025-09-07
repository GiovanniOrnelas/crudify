# Crudify API

Este é um projeto pessoal desenvolvido em **.NET** onde estou aplicando na prática o que venho estudando nos últimos meses sobre **arquitetura de software, boas práticas, autenticação, banco de dados e testes**.

⚠️ O projeto ainda está em **desenvolvimento ativo**:
- O CRUD ainda não está finalizado.
- Pretendo aplicar **Redis** para cache, visando melhorar performance e escalabilidade.
- Alguns testes unitários já foram implementados, mas ainda precisam ser expandidos.

---

## 🚧 Status do Projeto

- [x] Configuração inicial  
- [x] Autenticação JWT  
- [x] Documentação Swagger  
- [x] Testes unitários básicos  
- [ ] CRUD completo  
- [ ] Redis para cache  
- [ ] Testes de integração  
- [ ] Deploy (Docker / Cloud)  

---

## 📋 Pré-requisitos

Antes de rodar o projeto, certifique-se de ter instalado:

- [.NET 6+ SDK](https://dotnet.microsoft.com/download)  
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) (local ou via Docker)  
- [Postman](https://www.postman.com/downloads/) *(opcional, para testes de API)*  

---

## 🚀 Tecnologias utilizadas

- **ASP.NET Core Web API**
- **Entity Framework Core** (Code First + Migrations)
- **SQL Server** como banco de dados principal
- **JWT** para autenticação e autorização
- **Swagger / OpenAPI** para documentação
- **xUnit** para testes unitários
- **Redis** (planejado) para cache distribuído
- **Postman** para testes de endpoints

---

## 📂 Estrutura do Projeto

- **Crudify.App** → camada de aplicação (serviços, validações, regras de negócio)
- **Crudify.Domain** → entidades e abstrações
- **Crudify.Infrastructure** → configurações, repositórios EF Core, injeção de dependências
- **Crudify.Controllers** → endpoints REST da API
- **Crudify.Test** → testes unitários com xUnit

---

## 🔗 Endpoints para testes locais

- **Swagger UI**: [https://localhost:44314/swagger/index.html](https://localhost:44314/swagger/index.html)
- **URL Base**: `https://localhost:44314/{RouteController}`

---

## 📌 Exemplo de requisição

**Gerar token de autenticação (cURL):**

```bash
curl --location 'https://localhost:44314/api/v1/authentication' \
--header 'Content-Type: application/json' \
--data-raw '{
    "email": "admin@crudify.com",
    "password": "admin123"
}'
```

**Buscar tenants (cURL):**

```bash
curl --location 'https://localhost:44314/api/v1/tenant' \
--header 'Authorization: Bearer {{token}}'