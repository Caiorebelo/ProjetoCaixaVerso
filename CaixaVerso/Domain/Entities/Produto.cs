namespace CaixaVerso.Domain.Entities
{
    public class Produto
    {
        // Construtor público sem parâmetros (necessário para EF Core)
        public Produto() { }

        // Construtor auxiliar para facilitar criação em testes
        public Produto(int id, string nome, string tipo, decimal rentabilidade, string risco)
        {
            Id = id;
            Nome = nome;
            Tipo = tipo;
            Rentabilidade = rentabilidade;
            Risco = risco;
        }

        // Propriedades com get/set públicos
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public decimal Rentabilidade { get; set; }
        public string Risco { get; set; } = string.Empty;

        // Campos adicionais
        public int PrazoMinMeses { get; set; }
        public int PrazoMaxMeses { get; set; }
        public decimal ValorMin { get; set; }
        public decimal ValorMax { get; set; }
    }
}
