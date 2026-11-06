var construtor = WebApplication.CreateBuilder(args);

construtor.Services.AddControllers();

var aplicacao = construtor.Build();

aplicacao.UseHttpsRedirection();
aplicacao.MapControllers();

aplicacao.Run();
