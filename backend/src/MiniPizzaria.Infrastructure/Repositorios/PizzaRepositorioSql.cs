using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MiniPizzaria.Application.Excecoes;
using MiniPizzaria.Application.Repositorios;
using MiniPizzaria.Domain;
using MiniPizzaria.Infrastructure.Persistencia;

namespace MiniPizzaria.Infrastructure.Repositorios;

public class PizzaRepositorioSql(PizzariaDbContext db) : IPizzaRepositorio
{
    public async Task<IReadOnlyList<Pizza>> ObterTodasAsync()
        => await db.Pizzas.ToListAsync();

    public Task<Pizza?> ObterPorIdAsync(Guid id)
        => db.Pizzas.FirstOrDefaultAsync(p => p.Id == id);

    public async Task AdicionarAsync(Pizza pizza)
    {
        db.Pizzas.Add(pizza);
        try
        {
            await db.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException { Number: 2601 or 2627 })
        {
            throw new PizzaDuplicadaExcecao(pizza.Nome);
        }
    }

    public Task<Pizza?> ObterPorNomeAsync(string nome) => db.Pizzas.FirstOrDefaultAsync(p => p.Nome == nome);
    
    public Task<bool> ExisteComNomeAsync(string nome)
        => db.Pizzas.AnyAsync(p => p.Nome == nome);
}