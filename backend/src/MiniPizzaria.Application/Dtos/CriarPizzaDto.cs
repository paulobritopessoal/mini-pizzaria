namespace MiniPizzaria.Domain;

public record CriarPizzaDto(string Nome, decimal Preco, List<Ingrediente> Ingredientes);