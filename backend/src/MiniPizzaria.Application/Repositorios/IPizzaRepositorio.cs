using MiniPizzaria.Domain;

namespace MiniPizzaria.Application;

public interface IPizzaRepositorio
{
    Task ObterTodasAsync();
    Task ObterPorIdAsync(Guid id);
    Task AdicionarAsync(Pizza pizza);
}