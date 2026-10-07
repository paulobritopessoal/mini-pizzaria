using MiniPizzaria.Application.Dtos;

namespace MiniPizzaria.Application.Servicos;

public interface IPizzaService
{
    Task<IReadOnlyList<PizzaDto>> ObterTodasAsync();
    Task<PizzaDto?> ObterPorIdAsync(Guid id);
    Task<PizzaDto> CriarAsync(CriarPizzaDto dto);
}