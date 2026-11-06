# RetroGames

Marketplace de jogos retrô, com catálogo público e área administrativa para manutenção de plataformas e jogos.

Projeto acadêmico individual de **Adauclever Pacheco Lino**, desenvolvido conforme os roteiros da disciplina.

## Estado do projeto

| Etapa | Implementação |
| --- | --- |
| Aula 1 | Solução .NET com seis projetos, frontend React/Vite, estrutura do banco, dados de exemplo, consultas SQL e modelos de domínio. |
| Aula 2 | Base ADO.NET, repositórios de plataformas, jogos, vínculos jogo-gênero, gêneros e condições, com interfaces e `RepositorySet`. |

As regras de negócio, os endpoints da API, a autenticação e as telas do catálogo serão desenvolvidos nas próximas etapas. O frontend está no modelo inicial do Vite. O projeto xUnit está configurado, ainda sem testes unitários.

## Tecnologias e arquitetura

- .NET 10 e ASP.NET Core com controllers.
- ADO.NET com MySqlConnector 2.6.2.
- MySQL 8 com Docker Compose.
- React 19, Vite 8 e JavaScript.
- xUnit v3 para os testes unitários das próximas etapas.

A arquitetura prevista segue o fluxo **Controller → Service → Repository → MySQL**. Os Repositories executam SQL parametrizado e consultam uma tabela por vez. As regras de negócio e a composição dos dados com LINQ pertencem ao Service.

```text
Backend/
  RetroGames.sln
  RetroGames.Model/
  RetroGames.Repository/
  RetroGames.Services/
  RetroGames.WebApi.ApiModels/
  RetroGames.WebApi/
  RetroGames.WebApi.UnitTests/
Front-end/retrogames.web/
scripts/database/
scripts/consultas/
docs/
```

## Como executar

Pré-requisitos: **.NET SDK 10**, **Node.js 22 com npm** e **Docker Desktop** iniciado. O MySQL Workbench pode ser usado para consultar e inspecionar o banco.

Clone o repositório:

```bash
git clone https://github.com/Adauclever/RetroGames-Individual.git
cd RetroGames-Individual
```

Copie `.env.example` para `.env`. No PowerShell:

```powershell
Copy-Item .env.example .env
```

Substitua as duas ocorrências de `SUA_SENHA` pela mesma senha local. O `.env` é ignorado pelo Git.

Na raiz, inicie o banco e compile a solução:

```bash
docker compose up -d --wait
dotnet build Backend/RetroGames.sln
```

O banco `retrogames_individual` estará disponível em **localhost:3307**, com usuário `root` e a senha definida no `.env`. Os scripts de estrutura e dados são executados automaticamente na primeira criação do volume.

Inicie o frontend:

```bash
cd Front-end/retrogames.web
npm ci
npm run dev
```

Acesse [localhost:5173](http://localhost:5173). Para compilar o frontend, execute `npm run build`.

A estrutura inicial da API pode ser iniciada a partir da raiz:

```bash
dotnet run --project Backend/RetroGames.WebApi --urls http://localhost:5080
```

## Banco de dados

O banco contém as tabelas `tb_condicao_jogo`, `tb_perfil`, `tb_plataforma`, `tb_genero`, `tb_jogo`, `tb_jogo_genero` e `tb_usuario`.

Os dados iniciais incluem seis plataformas, oito gêneros, 20 jogos e 40 associações jogo-gênero. Há uma plataforma sem jogos, um jogo inativo e um jogo sem estoque. A tabela de usuários permanece vazia nesta etapa.

- [Estrutura e dados](scripts/database/)
- [Consultas e testes de integridade](scripts/consultas/)
- [Modelo de dados](docs/MER.md)

Os exercícios SQL da Aula 1 utilizam JOIN para consulta do modelo. Nos Repositories da Aula 2, cada consulta acessa uma única tabela.

## Verificações

Com o banco iniciado, execute na raiz:

```bash
python scripts/validar_banco.py
dotnet run --project scripts/VerificarRepositorios
```

O primeiro comando requer Python 3 e verifica consultas, restrições de integridade e recriação dos scripts em bancos descartáveis. O segundo executa 32 verificações de integração dos Repositories, incluindo filtros, paginação, gravações, duplicidade e rollback. Os dados temporários são revertidos.

A solução .NET e o frontend foram compilados com sucesso. As consultas foram verificadas pelo cliente MySQL no Docker. O acompanhamento detalhado está em [Progresso das aulas](docs/PROGRESSO_AULAS.md), e as funcionalidades previstas estão nas [Issues](https://github.com/Adauclever/RetroGames-Individual/issues).

## Desenvolvimento

O projeto utiliza apoio de assistente de código conforme as diretrizes do [AGENTS.md](AGENTS.md). As verificações executadas e as limitações de cada etapa estão documentadas no repositório.
