namespace MiniPizzaria.Application.Dtos;

public record CriarPizzaDto(string Nome, decimal Preco, List<IngredienteDto> Ingredientes);