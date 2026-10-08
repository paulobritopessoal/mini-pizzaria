using Microsoft.EntityFrameworkCore;
using MiniPizzaria.Domain;

namespace MiniPizzaria.Infrastructure.Persistencia;

public class PizzariaDbContext(DbContextOptions<PizzariaDbContext> options) : DbContext(options)
{
    public DbSet<Pizza> Pizzas => Set<Pizza>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pizza>(p =>
        {
            p.HasKey(x => x.Id);
            p.Property(x => x.Nome).HasMaxLength(100).IsRequired();
            p.HasIndex(p=>p.Nome).IsUnique();
            p.Property(x => x.Preco).HasPrecision(10, 2);

            // Os ingredientes vão para uma tabela própria, "presa" à pizza
            p.OwnsMany(x => x.Ingredientes, i =>
            {
                i.ToTable("PizzaIngredientes");
                i.Property(x => x.Nome).HasMaxLength(50).IsRequired();
            });

            // Diz ao EF Core para usar a lista privada _ingredientes
            p.Navigation(x => x.Ingredientes).UsePropertyAccessMode(PropertyAccessMode.Field);
        });
    }
}