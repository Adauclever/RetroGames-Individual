using RetroGames.Model.Model;
namespace RetroGames.Repository;
public class GeneroRepository(RepositoryConfig configuracao) : RepositoryBase(configuracao), IGeneroRepository
{
    public async Task<List<Genero>> ListarAsync()
    {
        await using var conexao = await AbrirConexaoAsync();
        await using var comando = CriarComando(conexao,"SELECT co_seq_genero,no_genero FROM tb_genero ORDER BY no_genero");
        await using var leitor = await comando.ExecuteReaderAsync();
        var registros = new List<Genero>();
        while (await leitor.ReadAsync()) registros.Add(new() { Id=leitor.GetInt32("co_seq_genero"),Nome=leitor.GetString("no_genero") });
        return registros;
    }
}
