using System.Transactions;
using RetroGames.Model.Model;
using RetroGames.Repository;

var diretorio = new DirectoryInfo(Directory.GetCurrentDirectory());
while (diretorio is not null && !File.Exists(Path.Combine(diretorio.FullName, ".env"))) diretorio = diretorio.Parent;
if (diretorio is null) throw new InvalidOperationException("Execute a partir do repositório individual com .env configurado.");
var linha = File.ReadLines(Path.Combine(diretorio.FullName, ".env"))
    .Single(valor => valor.StartsWith("ConnectionStrings__Default="));
var configuracao = new RepositoryConfig { ConnectionString = linha.Split('=', 2)[1].Trim('"') };
IRepositorySet repositorios = new RepositorySet(new PlataformaRepository(configuracao), new JogoRepository(configuracao),
    new JogoGeneroRepository(configuracao), new GeneroRepository(configuracao), new CondicaoJogoRepository(configuracao));
var verificacoes = 0;
void Conferir(bool condicao, string mensagem)
{
    if (!condicao) throw new InvalidOperationException(mensagem);
    verificacoes++;
}
var plataformas = await repositorios.Plataforma.ListarAsync();
Conferir(plataformas.Count == 6, "Esperadas seis plataformas.");
Conferir((await repositorios.Plataforma.BuscarPorIdAsync(1))?.Nome == "NES", "Busca de plataforma.");
Conferir(await repositorios.Plataforma.BuscarPorIdAsync(-1) is null, "Plataforma inexistente.");
Conferir(await repositorios.Plataforma.ExisteComNomeAsync("NES"), "Nome existente.");
Conferir(!await repositorios.Plataforma.ExisteComNomeAsync("NES",1), "Ignorar próprio id.");
Conferir(await repositorios.Plataforma.PossuiJogosAsync(1), "Plataforma com jogos.");
Conferir(!await repositorios.Plataforma.PossuiJogosAsync(6), "Plataforma sem jogos.");
var primeira = await repositorios.Jogo.ListarAsync();
var segunda = await repositorios.Jogo.ListarAsync(pagina:2);
Conferir(primeira.Total == 19 && primeira.Jogos.Count == 5, "Paginação e exclusão de inativos.");
Conferir(!primeira.Jogos.Select(jogo=>jogo.Id).Intersect(segunda.Jogos.Select(jogo=>jogo.Id)).Any(), "Páginas distintas.");
Conferir((await repositorios.Jogo.ListarAsync(incluirInativos:true)).Total == 20, "Incluir inativos.");
Conferir((await repositorios.Jogo.ListarAsync(nome:"Mario")).Total == 5, "Busca textual.");
Conferir((await repositorios.Jogo.ListarAsync(idPlataforma:3)).Total == 5, "Filtro de plataforma.");
Conferir((await repositorios.Jogo.ListarAsync(ids:[1,2],incluirInativos:true)).Total == 2, "Filtro de ids.");
Conferir((await repositorios.Jogo.ListarAsync(ids:[])).Total == 0, "Lista de ids vazia.");
Conferir((await repositorios.Jogo.ListarAsync(nome:"' OR 1=1 --")).Total == 0, "Parâmetros impedem injeção SQL.");
Conferir((await repositorios.Jogo.BuscarPorIdAsync(1))?.Descricao is null, "Descrição anulável.");
Conferir(await repositorios.Jogo.BuscarPorIdAsync(-1) is null, "Jogo inexistente.");
Conferir((await repositorios.JogoGenero.ListarTodosAsync()).Count == 40, "Todos os vínculos.");
Conferir((await repositorios.JogoGenero.ListarPorJogosAsync([1,2])).Count == 4, "Vínculos por jogos.");
Conferir((await repositorios.JogoGenero.ListarPorJogosAsync([])).Count == 0, "Lista vazia de jogos.");
Conferir((await repositorios.Genero.ListarAsync()).Count == 8, "Gêneros.");
Conferir((await repositorios.CondicaoJogo.ListarAsync()).Count == 3, "Condições.");

// O teste controla a transação. Repositories apenas participam dela, como farão com o Service.
using (var transacao = new TransactionScope(TransactionScopeOption.Required,
    new TransactionOptions { IsolationLevel=IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
{
    var plataforma = new Plataforma { Nome="Plataforma de teste",Fabricante="Teste",AnoLancamento=1990 };
    plataforma.Id = await repositorios.Plataforma.InserirAsync(plataforma);
    plataforma.Nome = "Plataforma alterada";
    await repositorios.Plataforma.AlterarAsync(plataforma);
    Conferir((await repositorios.Plataforma.BuscarPorIdAsync(plataforma.Id))?.Nome == plataforma.Nome,"Alteração de plataforma.");
    var jogo = new Jogo { Nome="Jogo de teste",Preco=0,Estoque=1,Ativo=true,Cadastro=DateTime.UtcNow,IdPlataforma=plataforma.Id,IdCondicaoJogo=2,AnoLancamento=1990 };
    jogo.Id = await repositorios.Jogo.InserirAsync(jogo);
    jogo.Descricao = "Descrição de teste";
    jogo.Nome = "Jogo alterado";
    await repositorios.Jogo.AlterarAsync(jogo);
    Conferir((await repositorios.Jogo.BuscarPorIdAsync(jogo.Id))?.Descricao == jogo.Descricao,"Alteração de jogo.");
    await repositorios.JogoGenero.InserirAsync(jogo.Id,[1,2]);
    Conferir((await repositorios.JogoGenero.ListarPorJogosAsync([jogo.Id])).Count == 2,"Gravação de vínculos.");
    await repositorios.JogoGenero.RemoverPorJogoAsync(jogo.Id);
    Conferir((await repositorios.JogoGenero.ListarPorJogosAsync([jogo.Id])).Count == 0,"Remoção de vínculos.");
    await repositorios.Jogo.DesativarAsync(jogo.Id);
    Conferir((await repositorios.Jogo.BuscarPorIdAsync(jogo.Id))?.Ativo == false,"Exclusão lógica.");
    var temporaria = new Plataforma { Nome="Temporária",Fabricante="Teste",AnoLancamento=1990 };
    temporaria.Id=await repositorios.Plataforma.InserirAsync(temporaria);
    await repositorios.Plataforma.ExcluirAsync(temporaria.Id);
    Conferir(await repositorios.Plataforma.BuscarPorIdAsync(temporaria.Id) is null,"Exclusão de plataforma sem jogos.");
    // Sem Complete: todas as gravações de teste são revertidas.
}
try
{
    await repositorios.Plataforma.InserirAsync(new() { Nome="NES",Fabricante="Nintendo",AnoLancamento=1983 });
    throw new InvalidOperationException("Duplicidade deveria ter sido recusada.");
}
catch (RegistroDuplicadoException) { verificacoes++; }
try
{
    await repositorios.Plataforma.AlterarAsync(new() { Id=6,Nome="NES",Fabricante="Teste",AnoLancamento=1983 });
    throw new InvalidOperationException("Duplicidade no UPDATE deveria ter sido recusada.");
}
catch (RegistroDuplicadoException) { verificacoes++; }
Conferir((await repositorios.Plataforma.ListarAsync()).Count == 6,"Rollback de plataformas.");
Conferir((await repositorios.Jogo.ListarAsync(incluirInativos:true)).Total == 20,"Rollback de jogos.");
Console.WriteLine($"{verificacoes} verificações de integração passaram. Nenhum dado de teste permaneceu.");
