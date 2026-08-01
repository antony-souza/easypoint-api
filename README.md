# EasyPoint API

Backend central do EasyPoint, responsável pelas regras de negócio, persistência dos dados, autenticação, sincronização e comunicação entre os PDVs e o painel administrativo.

## Responsabilidade

A API é o **núcleo central do sistema** e a principal autoridade sobre os dados.

É responsável por:

* Regras de negócio
* Autenticação e autorização
* Produtos
* Preços
* Estoque
* Lotes
* Vendas
* Pagamentos
* Lojas
* Caixas
* Usuários
* Relatórios e consultas
* Sincronização dos PDVs
* Processamento de eventos
* Comunicação em tempo real
* Integração com serviços externos

O painel administrativo e os PDVs não acessam diretamente o banco PostgreSQL. Toda comunicação com o banco central passa pela API.

## Arquitetura

A base utiliza um **monólito modular com Clean Architecture**. O sistema continua
sendo publicado como uma única API, enquanto as regras são separadas por módulos
de negócio. Essa abordagem reduz a complexidade operacional no início e preserva
fronteiras que poderão ser extraídas para serviços independentes no futuro.

```text
EasyPoint.Api
    │
    ├── EasyPoint.Application
    │         │
    │         └── EasyPoint.Domain
    │
    └── EasyPoint.Infrastructure
              │
              ├── EasyPoint.Application
              └── EasyPoint.Domain
```

Regra principal de dependência:

* `Domain` não depende de nenhum outro projeto.
* `Application` depende somente de `Domain`.
* `Infrastructure` implementa contratos definidos por `Application`.
* `Api` é o ponto de entrada e a raiz de composição.

Os módulos iniciais são `Catalog`, `Inventory`, `Sales`, `Payments`, `Stores`,
`Identity`, `Synchronization` e `Reporting`.

A descrição completa está em [docs/architecture.md](docs/architecture.md).

## Estrutura

```text
easypoint-api/
├── src/
│   ├── EasyPoint.Api/
│   ├── EasyPoint.Application/
│   ├── EasyPoint.Domain/
│   └── EasyPoint.Infrastructure/
├── tests/
│   ├── EasyPoint.UnitTests/
│   ├── EasyPoint.IntegrationTests/
│   └── EasyPoint.ArchitectureTests/
├── deploy/
├── docs/
├── Directory.Build.props
└── EasyPoint.slnx
```

O projeto está propositalmente sem entidades, casos de uso ou endpoints de
exemplo. A estrutura inicial define apenas as fronteiras arquiteturais.

## Aplicação

```text
EasyPoint Admin
       │
       │ HTTPS
       ▼
┌───────────────────┐
│   EasyPoint API   │
│    ASP.NET Core   │
└─────────┬─────────┘
          │
       EF Core
          │
          ▼
      PostgreSQL
```

Para eventos e sincronização:

```text
API
 │
 ├── PostgreSQL
 │
 └── RabbitMQ
       ↓
    Consumer
       ↓
    SignalR
       ↓
      PDVs
```

## Sincronização

A API mantém o estado oficial dos dados.

Quando uma informação é alterada, o backend pode registrar um evento de sincronização e notificar os PDVs conectados através do SignalR.

PDVs que estavam offline podem verificar posteriormente quais alterações perderam e atualizar seus bancos SQLite.

O sistema utiliza uma abordagem **offline-first no PDV e centralizada no backend**.

## Stack

* **C#**
* **ASP.NET Core**
* **Entity Framework Core**
* **PostgreSQL**
* **RabbitMQ**
* **SignalR**
* **REST API**
* **Docker**

## Infraestrutura local com Docker Compose

O arquivo `docker-compose.yml` sobe as dependências usadas pela API:

* PostgreSQL em `localhost:5432`
* RabbitMQ em `localhost:5672`, com painel em <http://localhost:15672>
* Redis em `localhost:6379`

Para iniciar o ambiente de desenvolvimento:

```powershell
Copy-Item .env.example .env
docker compose up -d
docker compose ps
```

O arquivo `.env` é ignorado pelo Git. Edite-o para trocar credenciais ou
portas sem alterar o Compose. Os dados ficam em volumes nomeados e continuam
disponíveis após reiniciar os containers.

Quando a API for executada dentro de um container, use `postgres`, `rabbitmq` e
`redis` como hosts internos da rede Compose; o `.env.example` usa `localhost`
porque a API, neste primeiro passo, roda diretamente no host.

## Princípio

> **O PostgreSQL é a fonte oficial dos dados e a API é a autoridade central do sistema.**
