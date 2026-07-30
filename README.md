# EasyPoint API

Backend central da plataforma **EasyPoint — Ponto Fácil**, responsável por fornecer a API que integra o sistema de PDV e o painel administrativo.

## 🎯 Propósito

O `easypoint-api` centraliza as regras de negócio e o gerenciamento das informações da plataforma.

É responsável por:

* Gerenciamento de produtos e preços
* Controle de estoque
* Registro e gerenciamento de vendas
* Gerenciamento de caixas
* Usuários e permissões
* Autenticação e autorização
* Comunicação com o banco de dados
* Validação das operações realizadas pelo PDV e pelo painel administrativo

## 🏗️ Arquitetura

```text
                    EasyPoint API
                         │
          ┌──────────────┴──────────────┐
          │                             │
          ▼                             ▼
    EasyPoint PDV                 EasyPoint Admin
       WPF / C#                   React / TypeScript
          │                             │
          └──────────────┬──────────────┘
                         │
                      HTTP/HTTPS
                         │
                         ▼
                  EasyPoint API
                         │
                         ▼
                    PostgreSQL
```

## 🛠️ Tecnologias

* C#
* ASP.NET Core
* Entity Framework Core
* PostgreSQL
* Docker
* REST API

## 📌 Responsabilidade

O backend é a **fonte central da verdade** do EasyPoint.

Clientes como o PDV e o painel administrativo não acessam diretamente o banco de dados. Todas as operações passam pela API.

## 🚧 Status

Em desenvolvimento.

## 🔗 Projetos relacionados

* `easypoint-pdv` — aplicação desktop para operação do caixa
* `easypoint-admin` — painel web para gerenciamento da plataforma
