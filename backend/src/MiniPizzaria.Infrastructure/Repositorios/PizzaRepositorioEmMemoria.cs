using MiniPizzaria.Application.Repositorios;
using MiniPizzaria.Domain;

namespace MiniPizzaria.Infrastructure.Repositorios;

public class PizzaRepositorioEmMemoria : IPizzaRepositorio
{
    private readonly List<Pizza> _pizzas = new();

    public Task<IReadOnlyList<Pizza>> ObterTodasAsync()
        => Task.FromResult<IReadOnlyList<Pizza>>(_pizzas.AsReadOnly());

    public Task<Pizza?> ObterPorIdAsync(Guid id)
    {
        return Task.FromResult(_pizzas.FirstOrDefault(p => p.Id == id));
    }

    public Task AdicionarAsync(Pizza pizza)
    {
        if(_pizzas.Any(p=>string.Equals(p.Nome, pizza.Nome, StringComparison.OrdinalIgnoreCase)))
        {
            throw new ArgumentException($"Já existe uma Pizza com o nome '{pizza.Nome}'"); 
        }
        _pizzas.Add(pizza);
        return Task.CompletedTask;
    }
}