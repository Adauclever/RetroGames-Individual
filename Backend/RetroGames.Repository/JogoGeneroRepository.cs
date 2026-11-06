using RetroGames.Model.Model;
namespace RetroGames.Repository;
public class JogoGeneroRepository(RepositoryConfig configuracao) : RepositoryBase(configuracao), IJogoGeneroRepository
{
    public Task<List<JogoGenero>> ListarTodosAsync() => ConsultarAsync(null);
    public Task<List<JogoGenero>> ListarPorJogosAsync(IReadOnlyCollection<int> idsJogos) => ConsultarAsync(idsJogos);
    private async Task<List<JogoGenero>> ConsultarAsync(IReadOnlyCollection<int>? idsJogos)
    {
        await using var conexao = await AbrirConexaoAsync();
        await using var comando = CriarComando(conexao,"SELECT co_jogo,co_genero FROM tb_jogo_genero");
        if (idsJogos is not null)
        {
            var parametros = idsJogos.Select((id, indice) =>
            {
                var parametro="@id"+indice;
                AdicionarParametro(comando,parametro,id);
                return parametro;
            }).ToArray();
            comando.CommandText += parametros.Length == 0 ? " WHERE 1=0" : " WHERE co_jogo IN ("+string.Join(",",parametros)+")";
        }
        comando.CommandText += " ORDER BY co_jogo,co_genero";
        await using var leitor = await comando.ExecuteReaderAsync();
        var vinculos = new List<JogoGenero>();
        while (await leitor.ReadAsync()) vinculos.Add(new() { IdJogo=leitor.GetInt32("co_jogo"),IdGenero=leitor.GetInt32("co_genero") });
        return vinculos;
    }
    public async Task InserirAsync(int idJogo,IReadOnlyCollection<int> idsGeneros)
    {
        if (idsGeneros.Count == 0) return;
        await using var conexao = await AbrirConexaoAsync();
        await using var comando = CriarComando(conexao,string.Empty);
        AdicionarParametro(comando,"@jogo",idJogo);
        var valores=idsGeneros.Select((id,indice) =>
        {
            var parametro="@genero"+indice;
            AdicionarParametro(comando,parametro,id);
            return "(@jogo,"+parametro+")";
        });
        comando.CommandText="INSERT INTO tb_jogo_genero(co_jogo,co_genero) VALUES "+string.Join(",",valores);
        await ExecutarGravacaoAsync(comando);
    }
    public async Task RemoverPorJogoAsync(int idJogo)
    {
        await using var conexao = await AbrirConexaoAsync();
        await using var comando = CriarComando(conexao,"DELETE FROM tb_jogo_genero WHERE co_jogo=@jogo");
        AdicionarParametro(comando,"@jogo",idJogo);
        await comando.ExecuteNonQueryAsync();
    }
}
