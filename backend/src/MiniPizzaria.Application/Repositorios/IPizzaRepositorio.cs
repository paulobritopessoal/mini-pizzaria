using MiniPizzaria.Domain;

namespace MiniPizzaria.Application.Repositorios;

public interface IPizzaRepositorio
{
    Task<IReadOnlyList<Pizza>> ObterTodasAsync();
    Task<Pizza?> ObterPorIdAsync(Guid id);
    Task AdicionarAsync(Pizza pizza);
    Task<Pizza?> ObterPorNomeAsync(string nome);
    Task<bool> ExisteComNomeAsync(string nome);
}