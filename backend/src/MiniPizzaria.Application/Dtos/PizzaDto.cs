namespace MiniPizzaria.Application.Dtos;
public record PizzaDto(Guid Id, string Nome, decimal Preco, bool Vegetariana, IReadOnlyList<IngredienteDto> Ingredientes);
