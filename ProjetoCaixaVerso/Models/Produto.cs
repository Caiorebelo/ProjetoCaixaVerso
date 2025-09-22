
namespace ProjetoCaixaVerso.Models;
public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal TaxaJurosAnual { get; set; }
    public int PrazoMaximoMeses { get; set; }
}