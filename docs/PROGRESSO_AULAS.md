# Progresso verificado — 06/11/2026

## Aula 1

1. Repositório individual público, Git independente, `.gitignore`, AGENTS na raiz e seis Issues criados. **Pendente:** quadro GitHub Projects, pois a credencial atual não possui escopo `project`. A instalação de VS Code/C# Dev Kit não foi inspecionada.
2. Seis projetos .NET 10 com referências conforme especificação, React/Vite inicial instalado e compilado. Frontend respondeu HTTP 200 em 127.0.0.1:5173. Criação inicial com pnpm; Node 22 portátil oficial, com SHA256 conferido, preparado para a validação final com npm. Node global preservado.
3. Sete tabelas criadas no MySQL 8. Os scripts contêm PK, FK padrão, NOT NULL, UNIQUE e índice em nome do jogo. A definição física é documentada em MER.md. **Inspeção visual no Table Inspector do Workbench não realizada.**
4. Seis plataformas, oito gêneros, 20 jogos, 40 vínculos; um inativo, um sem estoque, usuários vazios. Confirmado no banco.
5. Consultas executadas pelo cliente MySQL no Docker. Testes confirmaram erros 1452, 1062, 1451 e 1048; preço zero aceito. Gravações de teste revertidas. Resultados anotados no SQL.
6. Sete modelos com coluna comentada por propriedade, nulabilidade e constantes de condição/perfil correspondentes aos dados inseridos.

## Aula 2

1. MySqlConnector 2.6.2, RepositoryConfig, RepositoryBase e RegistroDuplicadoException implementados. Erro 1062 convertido apenas em ExecutarGravacaoAsync. Recursos descartados com await using.
2. Connection string local em `.env` ignorado pelo Git; modelo versionado com placeholder. A consulta de identificação do servidor foi executada pelo cliente MySQL, e a porta de acesso do host é 3307.
3. PlataformaRepository e interface com sete métodos implementados.
4. JogoRepository e JogoGeneroRepository implementados; filtros, contagem e paginação no SQL. Desativação é UPDATE; sem DELETE de jogo no Repository.
5. GeneroRepository, CondicaoJogoRepository, interfaces e RepositorySet com cinco propriedades implementados.

## Evidências

- `dotnet build Backend/RetroGames.sln`: sucesso, zero avisos e erros.
- `pnpm build`: sucesso.
- `npm install` e `npm run build` com Node 22.23.4/npm 10.9.9: sucesso.
- Estrutura e dados recriados duas vezes em schemas descartáveis; banco de desenvolvimento preservado.
- `python scripts/validar_banco.py`: integridade e contagens confirmadas.
- `dotnet run --project scripts/VerificarRepositorios`: 32 verificações passaram; sem dados temporários restantes.
- `scripts/consultas/03_repositories.sql`: executado no cliente MySQL com sucesso.
- Projeto xUnit alinhado à especificação v3; ainda sem testes unitários de Service/Controller, que não foram implementados nestas aulas.

Os testes de SQL foram feitos pela linha de comando, **não pela interface do Workbench**. Assim, o código dos roteiros está implementado, mas não se deve marcar o checklist inteiro como concluído antes da criação do quadro, da inspeção no Workbench e da conferência dos pré-requisitos locais pelo estudante.
