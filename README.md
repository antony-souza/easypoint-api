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

## Princípio

> **O PostgreSQL é a fonte oficial dos dados e a API é a autoridade central do sistema.**
