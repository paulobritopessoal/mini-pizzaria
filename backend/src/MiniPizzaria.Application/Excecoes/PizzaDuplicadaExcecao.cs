namespace MiniPizzaria.Application.Excecoes;

public class PizzaDuplicadaExcecao(string nome) : Exception($"Já existe uma pizza com o nome '{nome}'");