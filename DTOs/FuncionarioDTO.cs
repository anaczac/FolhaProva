using System.ComponentModel.DataAnnotations;

namespace Folha.DTOs;

public class FuncionarioDTO{

    [Required]
    public string? Nome { get; set; }
    [Required]
    public int Cpf { get; set; }
}