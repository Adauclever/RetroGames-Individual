# CONTEXTO_AGENTES.md — Memória e Alinhamento para IAs

Este arquivo registra o contexto compartilhado entre os assistentes de IA de todos os integrantes do grupo. Ele deve ser consultado antes de qualquer tarefa e atualizado a cada Pull Request/fase concluída.

---

## 1. Regra de Ouro para os Assistentes
Qualquer IA que atuar neste repositório **DEVE** seguir integralmente as diretrizes descritas em [.agents/agents.md](.agents/agents.md). Principais lembretes:
- **Linguagem:** Respostas, comentários, classes, métodos e variáveis sempre em **português**.
- **Stack Fixa:** .NET 10 (ASP.NET Core), ADO.NET (MySqlConnector), MySQL 8, FluentValidation, xUnit + Moq, React + Vite + Bootstrap + Axios, DotNetEnv. **Não adicionar ORMs (EF, Dapper), JWT, TypeScript nem Tailwind**.
- **Arquitetura:** 1 Repository por tabela, sem `JOIN` ou subconsultas entre tabelas no SQL (composição com LINQ no Service).
- **Convenções de Propriedades:** Propriedades em Models, ServiceModels e ApiModels não repetem o nome da classe (`Plataforma.Id`, `Plataforma.Nome`). PK é sempre `Id`; FK é `Id` + entidade (`Jogo.IdPlataforma`). Strings obrigatórias iniciam com `string.Empty`.
- **Segurança e Variáveis de Ambiente:** No curso, a aplicação pode conectar como `root` localmente. Segredos e connection string ficam em `.env` na raiz carregados com `DotNetEnv` (exemplo versionado em `.env.example`). Não usar `dotnet user-secrets` nem senhas em `appsettings*.json`.

---

## 2. Linha do Tempo e Decisões de Arquitetura

### Fase 1: Padronização do Ambiente de Desenvolvimento (Concluída)
- **Data:** 04/11/2026
- **Decisão:** Estratégia híbrida de ambiente e organização de branches.
  - **Branches:** Criada a branch estável `release`, a branch de integração `main` e uma branch de desenvolvimento dedicada para cada membro (`dev-santiago`, `dev-adauclever`, `dev-allan`, `dev-gabriel`, `dev-eurico`).
  - **Docker Compose:** gerencia exclusivamente o contêiner do **MySQL 8** na porta `3306`, com volume persistente e usuário dedicado (`retrogames_user`) e banco `retrogames_db`.
  - **Host local:** .NET SDK 10 e Node.js 22 instalados diretamente na máquina dos integrantes para agilidade de compilação, hot-reload e debugging.
  - **MySQL Workbench:** instalado na máquina host conectando via `localhost:3306`.
- **Arquivos criados:**
  - `docker-compose.yml`: Definição do serviço do banco de dados.
  - `scripts/database/00_init.sql`: Ponto de entrada para scripts de migração do banco.
  - `.gitignore`: Proteção de artefatos de build, segredos e volumes locais.
  - `README.md`: Documentação e guia de execução por fases para o grupo.

---

## 3. Estado Atual do Repositório
- **Fase atual:** Preparação do Ambiente e Estrutura Inicial.
- **Próximos passos previstos:**
  - Criação da Solution .NET 10 e seus projetos em camadas (`RetroGames.Model`, `RetroGames.Repository`, `RetroGames.Service`, `RetroGames.WebApi`, `RetroGames.Tests`).
  - Definição do modelo de dados (`docs/MER.md` e scripts de criação de tabelas).
  - Inicialização do frontend React (Vite + Bootstrap).

---

## 4. Instruções para os Assistentes ao Abrir/Revisar Pull Requests
1. Verifique se o código proposto respeita o [.agents/agents.md](.agents/agents.md).
2. Se houver adição de pacotes NuGet ou npm não autorizados, rejeite e aponte a regra.
3. Se houver nova tabela ou mudança estrutural, garanta que foi adicionado um novo script SQL em `scripts/database/` e documentado no `docs/MER.md`.
4. Mantenha este arquivo atualizado com qualquer decisão de arquitetura tomada.

