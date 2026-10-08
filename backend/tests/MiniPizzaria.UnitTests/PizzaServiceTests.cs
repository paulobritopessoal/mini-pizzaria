using MiniPizzaria.Application.Dtos;
using MiniPizzaria.Application.Servicos;
using MiniPizzaria.Application.Excecoes;

namespace MiniPizzaria.UnitTests;

public class PizzaServiceTests
{
    [Fact]
    public async Task CriarAsync_GuardaAPizzaNoRepositorio()
    {
        // Arrange
        var repositorio = new FakePizzaRepository();
        var service = new PizzaService(repositorio);
        var dto = new CriarPizzaDto("Margherita", 8.5m, [new IngredienteDto("Tomate", true)]);

        // Act
        var criada = await service.CriarAsync(dto);

        // Assert
        Assert.Single(repositorio.Pizzas);                 // ficou exatamente uma pizza guardada
        Assert.Equal(criada.Id, repositorio.Pizzas[0].Id);
    }

    [Fact]
    public async Task CriarAsync_NaoGuardaAPizzaNoRepositorioComValorNegativo()
    {
        // Arrange
        var repositorio = new FakePizzaRepository();
        var service = new PizzaService(repositorio);
        var dto = new CriarPizzaDto("Margherita", -8.5m, [new IngredienteDto("Tomate", true)]);

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(() => service.CriarAsync(dto));
        Assert.Empty(repositorio.Pizzas);
    }
    
    [Fact]
    public async Task CriarAsyncComIngredientesVegetarianos_DevolveDtoEvegetariana()
    {
        // Arrange
        var repositorio = new FakePizzaRepository();
        var service = new PizzaService(repositorio);
        var dto = new CriarPizzaDto("Margherita", 8.5m, [new IngredienteDto("Tomate", true)]);

        // Act
        var criada = await service.CriarAsync(dto);
        
        // Assert
        Assert.True(criada.Vegetariana);

    }
    
    
    [Fact]
    public async Task ObterPorIdAsync_IdInexistente_DevolveNull()
    {
        // Arrange
        var repositorio = new FakePizzaRepository();
        var service = new PizzaService(repositorio);
        Guid fakeId = Guid.NewGuid();
        var pizza = await service.ObterPorIdAsync(fakeId);
        Assert.Null(pizza);
    }
    
    
    [Fact]
    public async Task ObterPorIdAsync_PizzaIdReal()
    {
        // Arrange
        var repositorio = new FakePizzaRepository();
        var service = new PizzaService(repositorio);
        var dto = new CriarPizzaDto("Margherita", 8.5m, [new IngredienteDto("Tomate", true)]);

        // Act
        var criada = await service.CriarAsync(dto);
        var pizza = await service.ObterPorIdAsync(criada.Id);
        Assert.NotNull(pizza);
        Assert.Equal("Margherita", pizza.Nome);
        Assert.Equal(criada.Id, pizza.Id);
    }

    [Fact]
    public async Task LancaPizzaDuplicadaExcecao()
    {
        // Arrange
        var repositorio = new FakePizzaRepository();
        var service = new PizzaService(repositorio);
        var dto = new CriarPizzaDto("Margherita", 8.5m, [new IngredienteDto("Tomate", true)]);
        // Act
        var criada = await service.CriarAsync(dto);
        
        // Assert
        Assert.Equal(criada.Id, repositorio.Pizzas[0].Id);
        await Assert.ThrowsAsync<PizzaDuplicadaExcecao>(() => service.CriarAsync(dto));
        Assert.Single(repositorio.Pizzas);                 // ficou exatamente uma pizza guardada
    }
    [Fact]
    public async Task CriarDuasPizzasDiferentes()
    {
        // Arrange
        var repositorio = new FakePizzaRepository();
        var service = new PizzaService(repositorio);
        var dto = new CriarPizzaDto("Margherita", 8.5m, [new IngredienteDto("Tomate", true)]);
        var dto2 = new CriarPizzaDto("Vegan", 8.5m, [new IngredienteDto("Tomate", true)]);
        // Act
        var criada = await service.CriarAsync(dto);
        var criada2 = await service.CriarAsync(dto2);
        
        Assert.Equal(criada.Id, repositorio.Pizzas[0].Id);
        Assert.Equal(criada2.Id, repositorio.Pizzas[1].Id);
        Assert.Equal(2, repositorio.Pizzas.Count);
    }
    
    [Fact]
    public async Task CriarPizzaComPrecoNegativoENomeRepetido()
    {
        // Arrange
        var repositorio = new FakePizzaRepository();
        var service = new PizzaService(repositorio);
        var dto = new CriarPizzaDto("Margherita", 8.5m, [new IngredienteDto("Tomate", true)]);
        var dto2 = new CriarPizzaDto("Margherita", -8.5m, [new IngredienteDto("Tomate", true)]);
        // Act
        var criada = await service.CriarAsync(dto);
        
        // Act + Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => service.CriarAsync(dto2)
        );
    }
    
}