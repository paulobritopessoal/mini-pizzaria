namespace MiniPizzaria.Domain;

public class Pizza
{
    public Guid Id { get; } = Guid.NewGuid();
    private readonly List<Ingrediente> _ingredientes = new();
    public IReadOnlyList<Ingrediente> Ingredientes => _ingredientes;

    public string Nome { get; private set; }

    public decimal Preco { get; private set; }

    public Pizza(string nome, decimal preco, List<Ingrediente> ingredientes)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("O nome é obrigatório");
        if (preco <= 0) throw new ArgumentException("O preço tem de ser positivo");
        Nome = nome;
        Preco = preco;
        _ingredientes.AddRange(ingredientes);
        if (!_ingredientes.Any())
            throw new ArgumentException("A pizza não pode estar sem ingredientes");
    }
    
    public void AdicionarIngrediente(Ingrediente i) => _ingredientes.Add(i);

    public bool EVegetariana => _ingredientes.All(i => i.Vegetariano);
    
    public string Descricao() => $"Nome: {Nome}, Preço: {Preco:C}"
           + (EVegetariana ? " (vegetariana)" : "")
           + " - " + string.Join(", ", _ingredientes.Select(i => i.Nome));
}

