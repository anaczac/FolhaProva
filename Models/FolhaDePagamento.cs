namespace Folha.Models;

public class FolhaDePagamento
{
   
    public int FolhaId { get; set; }
    public decimal ValorHora { get; set; }
    public int QuantidadeHora { get; set; }
    public int Mes { get; set; }
    public int Ano { get; set; }
    public decimal SalarioBruto { get; set; }
    public decimal ImpostoRenda { get; set; }
    public decimal INSS { get; set; }
    public decimal FGTS { get; set; }
    public decimal SalarioLiquido { get; set; }

    // Chave estrangeira para o funcionário
    public int FuncionarioId { get; set; }

    // Propriedade de navegação para o funcionário
    public Funcionario Funcionario { get; set; }
}

    
