using Microsoft.AspNetCore.Mvc;
using MiniPizzaria.Application.Dtos;
using MiniPizzaria.Application.Servicos;

namespace MiniPizzaria.Api.Controller;

[ApiController]
[Route("api/[controller]")]                       // [controller] = "Pizzas" (nome da classe sem "Controller")
public class PizzasController(IPizzaService service) : ControllerBase
{
    [HttpGet]                                     // GET /api/pizzas
    public async Task<ActionResult<IReadOnlyList<PizzaDto>>> ObterTodas()
        => Ok(await service.ObterTodasAsync());

    [HttpGet("{id:guid}")]                        // GET /api/pizzas/3fa85f64-...
    public async Task<ActionResult<PizzaDto>> ObterPorId(Guid id)
    {
        var pizza = await service.ObterPorIdAsync(id);
        return pizza is null ? NotFound() : Ok(pizza);
    }

    [HttpPost]                                    // POST /api/pizzas
    public async Task<ActionResult<PizzaDto>> Criar(CriarPizzaDto dto)
    {
        try
        {
            var criada = await service.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = criada.Id }, criada);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}