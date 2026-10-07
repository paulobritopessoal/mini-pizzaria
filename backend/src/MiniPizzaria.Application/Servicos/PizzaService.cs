using MiniPizzaria.Application.Dtos;
using MiniPizzaria.Application.Repositorios;
using MiniPizzaria.Domain;

namespace MiniPizzaria.Application.Servicos;

public class PizzaService(IPizzaRepositorio repositorio) : IPizzaService
{
    public async Task<IReadOnlyList<PizzaDto>> ObterTodasAsync()
    {
        var pizzas = await repositorio.ObterTodasAsync();
        return pizzas.Select(ParaDto).ToList();
    }

    public async Task<PizzaDto?> ObterPorIdAsync(Guid id)
    {
        var pizza = await repositorio.ObterPorIdAsync(id);
        return pizza is null ? null : ParaDto(pizza);
    }

    public async Task<PizzaDto> CriarAsync(CriarPizzaDto dto)
    {
        // 1. Converter os ingredientes do pedido em ingredientes "a sério" do domínio
        var ingredientes = (dto.Ingredientes ?? [])
            .Select(i => new Ingrediente(i.Nome, i.Vegetariano))
            .ToList();

        // 2. Criar a pizza: é AQUI que as regras são verificadas (preço, nome, ingredientes)
        var pizza = new Pizza(dto.Nome, dto.Preco, ingredientes);

        // 3. Guardar
        await repositorio.AdicionarAsync(pizza);

        // 4. Devolver a versão "para o cliente"
        return ParaDto(pizza);
    }

    // Converte a entidade do domínio no DTO que sai da API
    private static PizzaDto ParaDto(Pizza p) => new(
        p.Id,
        p.Nome,
        p.Preco,
        p.EVegetariana,
        p.Ingredientes.Select(i => new IngredienteDto(i.Nome, i.Vegetariano)).ToList());
}