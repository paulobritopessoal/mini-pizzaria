using MiniPizzaria.Domain;

namespace MiniPizzaria.UnitTests;

public class PizzaTests
{
    private static List<Ingrediente> IngredientesValidos() =>
        [new("Tomate", true), new("Queijo", true)];

    private static List<Ingrediente> IngredientesValidos2() =>
        [new("Tomate", true), new("Queijo", true), new("Fiambre",false)];
    
    [Fact]
    public void PizzaValida_ECriadaComOsDadosCertos()
    {
        var pizza = new Pizza("Margherita", 8.5m, IngredientesValidos());

        Assert.Equal("Margherita", pizza.Nome);
        Assert.Equal(8.5m, pizza.Preco);
        Assert.Equal(2, pizza.Ingredientes.Count);
    }

    [Fact]
    public void PrecoNegativo_LancaExcecao()
    {
        Assert.Throws<ArgumentException>(() => new Pizza("Margherita", -5, IngredientesValidos()));
    }

    [Fact]
    public void PrecoZero_LancaExcecao()
    {
        Assert.Throws<ArgumentException>(() => new Pizza("Margherita", 0, IngredientesValidos()));
    }

    [Fact]
    public void NomeVazio_LancaExcecao()
    {
        Assert.Throws<ArgumentException>(()=> new Pizza("",12.54m, IngredientesValidos()));
    }

    [Fact]
    public void SemIngredientes_LancaExcecao()
    {
        Assert.Throws<ArgumentException>(() => new Pizza("Marg", 12.4m, new List<Ingrediente>()));        
    }

    [Fact]
    public void SoIngredientesVegetarianos_EVegetariana()
    {
        var pizza = new Pizza("Margherita", 8.5m, IngredientesValidos());
        Assert.True(pizza.EVegetariana);
        
    }

    [Fact]
    public void UmIngredienteNaoVegetariano()
    {
        var pizza2 = new Pizza("Combinado de Queijo e fiambre", 12.4m, IngredientesValidos2());
        Assert.False(pizza2.EVegetariana);
    }
    
    [Fact]
    public void LimparListaOriginal_NaoAfetaAPizza()
    {
        var ingredientes = IngredientesValidos2();
        var pizza = new Pizza("Combinado", 12.4m, ingredientes);

        ingredientes.Clear();

        Assert.Equal(3, pizza.Ingredientes.Count);
    }
}