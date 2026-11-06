using MySqlConnector;
using RetroGames.Model.Model;
namespace RetroGames.Repository;
public class JogoRepository(RepositoryConfig configuracao) : RepositoryBase(configuracao), IJogoRepository
{
    private const string Colunas = "co_seq_jogo,no_jogo,ds_jogo,nu_ano_lancamento,vl_preco,qt_estoque,st_ativo,dt_cadastro,co_plataforma,co_condicao_jogo";
    public async Task<(List<Jogo> Jogos, int Total)> ListarAsync(string? nome = null, int? idPlataforma = null,
        IReadOnlyCollection<int>? ids = null, bool incluirInativos = false, int pagina = 1, int tamanhoPagina = 5)
    {
        await using var conexao = await AbrirConexaoAsync();
        await using var comando = CriarComando(conexao, string.Empty);
        var filtro = " WHERE (@nome IS NULL OR no_jogo LIKE @nome) AND (@plataforma IS NULL OR co_plataforma=@plataforma) AND (@inativos OR st_ativo=TRUE)";
        AdicionarParametro(comando,"@nome",nome is null ? null : "%"+nome+"%");
        AdicionarParametro(comando,"@plataforma",idPlataforma);
        AdicionarParametro(comando,"@inativos",incluirInativos);
        if (ids is not null)
        {
            // Somente os nomes dos parâmetros são gerados; valores nunca entram no SQL.
            var parametros = ids.Select((id, indice) =>
            {
                var parametro = "@id"+indice;
                AdicionarParametro(comando,parametro,id);
                return parametro;
            }).ToArray();
            filtro += parametros.Length == 0 ? " AND 1=0" : " AND co_seq_jogo IN ("+string.Join(",",parametros)+")";
        }
        comando.CommandText = "SELECT COUNT(*) FROM tb_jogo"+filtro;
        var total = Convert.ToInt32(await comando.ExecuteScalarAsync());
        comando.CommandText = "SELECT "+Colunas+" FROM tb_jogo"+filtro+" ORDER BY no_jogo,co_seq_jogo LIMIT @limite OFFSET @inicio";
        AdicionarParametro(comando,"@limite",tamanhoPagina);
        AdicionarParametro(comando,"@inicio",(pagina-1)*tamanhoPagina);
        await using var leitor = await comando.ExecuteReaderAsync();
        var jogos = new List<Jogo>();
        while (await leitor.ReadAsync()) jogos.Add(Mapear(leitor));
        return (jogos,total);
    }
    public async Task<Jogo?> BuscarPorIdAsync(int id)
    {
        await using var conexao = await AbrirConexaoAsync();
        await using var comando = CriarComando(conexao,"SELECT "+Colunas+" FROM tb_jogo WHERE co_seq_jogo=@id");
        AdicionarParametro(comando,"@id",id);
        await using var leitor = await comando.ExecuteReaderAsync();
        return await leitor.ReadAsync() ? Mapear(leitor) : null;
    }
    public async Task<int> InserirAsync(Jogo jogo)
    {
        await using var conexao = await AbrirConexaoAsync();
        await using var comando = CriarComando(conexao,"INSERT INTO tb_jogo(no_jogo,ds_jogo,nu_ano_lancamento,vl_preco,qt_estoque,st_ativo,dt_cadastro,co_plataforma,co_condicao_jogo) VALUES(@nome,@descricao,@ano,@preco,@estoque,@ativo,@cadastro,@plataforma,@condicao)");
        Preencher(comando,jogo);
        await ExecutarGravacaoAsync(comando);
        return checked((int)comando.LastInsertedId);
    }
    public async Task AlterarAsync(Jogo jogo)
    {
        await using var conexao = await AbrirConexaoAsync();
        await using var comando = CriarComando(conexao,"UPDATE tb_jogo SET no_jogo=@nome,ds_jogo=@descricao,nu_ano_lancamento=@ano,vl_preco=@preco,qt_estoque=@estoque,st_ativo=@ativo,dt_cadastro=@cadastro,co_plataforma=@plataforma,co_condicao_jogo=@condicao WHERE co_seq_jogo=@id");
        Preencher(comando,jogo);
        AdicionarParametro(comando,"@id",jogo.Id);
        await ExecutarGravacaoAsync(comando);
    }
    public async Task DesativarAsync(int id)
    {
        await using var conexao = await AbrirConexaoAsync();
        await using var comando = CriarComando(conexao,"UPDATE tb_jogo SET st_ativo=FALSE WHERE co_seq_jogo=@id");
        AdicionarParametro(comando,"@id",id);
        await ExecutarGravacaoAsync(comando);
    }
    private void Preencher(MySqlCommand comando,Jogo jogo)
    {
        AdicionarParametro(comando,"@nome",jogo.Nome);
        AdicionarParametro(comando,"@descricao",jogo.Descricao);
        AdicionarParametro(comando,"@ano",jogo.AnoLancamento);
        AdicionarParametro(comando,"@preco",jogo.Preco);
        AdicionarParametro(comando,"@estoque",jogo.Estoque);
        AdicionarParametro(comando,"@ativo",jogo.Ativo);
        AdicionarParametro(comando,"@cadastro",jogo.Cadastro);
        AdicionarParametro(comando,"@plataforma",jogo.IdPlataforma);
        AdicionarParametro(comando,"@condicao",jogo.IdCondicaoJogo);
    }
    private static Jogo Mapear(MySqlDataReader leitor) => new()
    {
        Id=leitor.GetInt32("co_seq_jogo"), Nome=leitor.GetString("no_jogo"),
        Descricao=leitor.IsDBNull(leitor.GetOrdinal("ds_jogo")) ? null : leitor.GetString("ds_jogo"),
        AnoLancamento=leitor.GetInt32("nu_ano_lancamento"), Preco=leitor.GetDecimal("vl_preco"),
        Estoque=leitor.GetInt32("qt_estoque"), Ativo=leitor.GetBoolean("st_ativo"),
        Cadastro=DateTime.SpecifyKind(leitor.GetDateTime("dt_cadastro"),DateTimeKind.Utc),
        IdPlataforma=leitor.GetInt32("co_plataforma"), IdCondicaoJogo=leitor.GetInt32("co_condicao_jogo")
    };
}
