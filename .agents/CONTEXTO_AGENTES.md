# Contexto do RetroGames individual

Responsável: Adauclever Pacheco Lino.
Repositório: https://github.com/Adauclever/RetroGames-Individual
Histórico Git independente, branch main. O trabalho do grupo permanece no repositório original.
Seguir AGENTS.md da raiz e a especificação do professor.

## Estado em 06/11/2026

Aula 1: solução de seis projetos net10.0, referências, React 19/Vite 8 em JavaScript, sete tabelas, dados de exemplo, consultas e modelos implementados.
Aula 2: base ADO.NET e cinco Repositories, interfaces e RepositorySet implementados. MySqlConnector 2.6.2 apenas no projeto Repository.
xUnit v3 3.0.0 preparado; ainda não há testes unitários de Service/Controller.
Banco individual no Docker, MySQL 8, host localhost:3307, schema retrogames_individual. Scripts de inicialização rodam somente na primeira criação do volume.
Segredo gerado localmente no .env; não imprimir nem versionar. .env.example contém apenas placeholder.
API sem endpoints de negócio e sem carregamento de .env; essa configuração pertence à aula futura.
Frontend é o modelo inicial oficial do Vite.

## Validação e pendências

Build .NET sem erros/avisos, build Vite, cinco testes de integridade e 32 verificações de integração passaram. Consultas executadas pelo cliente MySQL do Docker, não pela interface do Workbench.
Quadro público https://github.com/users/Adauclever/projects/1 conferido: seis Issues de funcionalidades em Todo e dois cartões das aulas em Done. Conferir docs/PROGRESSO_AULAS.md antes de afirmar que todos os itens formais foram concluídos.
A inspeção do Table Inspector e de VS Code/C# Dev Kit não foi realizada.
Próxima aula: Services e regras de negócio, de acordo com novo roteiro fornecido pelo estudante.
