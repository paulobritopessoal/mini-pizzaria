namespace MiniPizzaria.Domain;

public record PizzaDto(Guid Id, string Nome, decimal Preco, bool Vegetariana, IReadOnlyList<Ingrediente> Ingredientes);
