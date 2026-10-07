namespace MiniPizzaria.UnitTests;
using MiniPizzaria.Application.Repositorios;
using MiniPizzaria.Domain;

public class FakePizzaRepositorio : IPizzaRepositorio
{
    public List<Pizza> Pizzas { get; } = new();   // público, para os testes poderem espreitar

    public Task<IReadOnlyList<Pizza>> ObterTodasAsync()
        => Task.FromResult<IReadOnlyList<Pizza>>(Pizzas);

    public Task<Pizza?> ObterPorIdAsync(Guid id)
        => Task.FromResult(Pizzas.FirstOrDefault(p => p.Id == id));

    public Task AdicionarAsync(Pizza pizza)
    {
        Pizzas.Add(pizza);
        return Task.CompletedTask;
    }
}