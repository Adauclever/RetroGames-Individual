using RetroGames.Model.Model;
namespace RetroGames.Repository;
public class CondicaoJogoRepository(RepositoryConfig configuracao) : RepositoryBase(configuracao), ICondicaoJogoRepository
{
    public async Task<List<CondicaoJogo>> ListarAsync()
    {
        await using var conexao = await AbrirConexaoAsync();
        await using var comando = CriarComando(conexao,"SELECT co_condicao_jogo,ds_condicao_jogo FROM tb_condicao_jogo ORDER BY ds_condicao_jogo");
        await using var leitor = await comando.ExecuteReaderAsync();
        var registros = new List<CondicaoJogo>();
        while (await leitor.ReadAsync()) registros.Add(new() { Id=leitor.GetInt32("co_condicao_jogo"),Descricao=leitor.GetString("ds_condicao_jogo") });
        return registros;
    }
}
