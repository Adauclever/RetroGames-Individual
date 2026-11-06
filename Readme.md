# RetroGames — projeto individual

Projeto acadêmico de **Adauclever Pacheco Lino**, separado do trabalho em grupo. Implementação das bases das Aulas 1 (ambiente, solução, banco e modelos) e 2 (Repository).

## Estrutura

- `Backend/RetroGames.sln`: seis projetos .NET 10 com as referências da seção 3.2.
- `Front-end/retrogames.web`: modelo inicial React 19 + Vite 8, em JavaScript.
- `scripts/database`: scripts numerados de estrutura e dados, executados apenas na primeira criação do volume.
- `scripts/consultas`: exercícios SQL e testes reproduzíveis no Workbench.
- `scripts/VerificarRepositorios`: verificação de integração com o MySQL real, fora da solução de seis projetos.
- `docs/MER.md`: relações e decisões de tipos do banco.
- `docs/PROGRESSO_AULAS.md`: evidências e pendências dos roteiros.

## Executar

Pré-requisitos: .NET SDK 10, Node.js 22 com npm e Docker Desktop. Há também um lockfile pnpm da criação inicial; prefira npm com o package-lock.json para reproduzir a verificação final.

Nesta máquina foi baixado Node 22 portátil em `.ferramentas` (ignorado pelo Git), sem mudar o Node global. No PowerShell, a partir da raiz, execute `. ./scripts/usar_node22.ps1` para ativá-lo nesta sessão. Em outro computador, instale Node 22 ou use a versão indicada em `.nvmrc`.

1. Copie `.env.example` para `.env` e substitua **ambas** as ocorrências de `SUA_SENHA` pela mesma senha local. Não versione `.env`.
2. Na raiz, execute `docker compose up -d --wait`. O banco individual é `retrogames_individual`, em `localhost:3307`; a porta interna é 3306.
3. Compile: `dotnet build Backend/RetroGames.sln`.
4. No diretório `Front-end/retrogames.web`, execute `npm ci` e `npm run dev`. Abra `http://localhost:5173`. Para gerar o build: `npm run build`.
5. Para a estrutura inicial da API: `dotnet run --project Backend/RetroGames.WebApi --urls http://localhost:5080`.

A API está configurada para controllers, mas ainda não possui endpoints de negócio. Services, ApiModels, autenticação e interface do catálogo pertencem às próximas aulas. O `.env` será carregado pela API na aula indicada pelo roteiro; por enquanto, somente o verificador de integração lê essa configuração.

## Verificar as Aulas 1 e 2

Com o banco iniciado e sem alterar os dados de exemplo:

```text
python scripts/validar_banco.py
dotnet run --project scripts/VerificarRepositorios
dotnet test Backend/RetroGames.sln
```

O primeiro comando exige Python 3 e o cliente Docker, verifica integridade e reverte os dados temporários. O segundo executa 32 verificações reais, incluindo paginação, listas vazias, SQL injection, CRUD, duplicidade e rollback. O projeto xUnit v3 foi preparado para as futuras aulas de testes; ainda não contém testes unitários.

No Workbench, conecte em `localhost:3307`, usuário `root`, usando a senha do seu `.env`. Abra os arquivos de `scripts/consultas`; nos testes com erro esperado, execute cada bloco separadamente e finalize com `ROLLBACK`. O SQL dos exercícios da Aula 1 usa JOIN; os Repositories da Aula 2 consultam uma tabela por vez.

O banco possui sete tabelas, seis plataformas, oito gêneros, 20 jogos, 40 vínculos, três condições e dois perfis. Usuários permanecem vazios; um jogo está inativo e outro sem estoque. Atari 2600 não tem jogos, permitindo verificar a contagem zero.

## Acompanhamento

Repositório: https://github.com/Adauclever/RetroGames-Individual

As seis funcionalidades do escopo estão cadastradas nas Issues. O quadro GitHub Projects depende da autorização adicional `project` na conta autenticada. Elas continuam abertas porque as próximas aulas implementarão os fluxos completos.

Há commits separados para as etapas. Nenhum arquivo ou histórico do repositório do grupo foi reaproveitado como histórico individual; a solução inicial foi copiada como base e os créditos desta origem são preservados aqui.
