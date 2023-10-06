using System.ComponentModel.DataAnnotations;

namespace Folha.DTOs;
public class FolhaDePagamentoDTO
{
    //DataAnnotations
    [Required]
    
    public decimal ValorHora { get; set; }
    public int QuantidadeHora { get; set; }
    public int Mes { get; set; }
    public int Ano { get; set; }
    public int FuncionarioId { get; set; }
}


