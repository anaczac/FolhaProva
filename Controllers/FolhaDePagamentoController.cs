using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Folha.Data;
using Folha.DTOs;
using Folha.Models;

using System.Collections.Generic;
using System.Linq;

[Route("api/[FolhaDePagamentoController]")]
[ApiController]
public class FolhaDePagamentoController : ControllerBase
{
    private readonly AppDataContext _ctx;

    public FolhaDePagamentoController(AppDataContext context)
    {
        _ctx = context;
    }

    // POST: api/FolhaDePagamento
    [HttpPost]
    public IActionResult CalcularFolhaDePagamento(FolhaDePagamento folhaDePagamento)
    {
        // Implemente os cálculos de folha de pagamento aqui
        // ...

        // Após o cálculo, você deve salvar a folha de pagamento no banco de dados
        folhaDePagamento.SalarioBruto = folhaDePagamento.ValorHora * folhaDePagamento.QuantidadeHora;

    // Cálculo do Imposto de Renda
    decimal salarioBruto = folhaDePagamento.SalarioBruto;
    if (salarioBruto <= 1903.98m)
    {
        folhaDePagamento.ImpostoRenda = 0;
    }
    else if (salarioBruto <= 2826.65m)
    {
        folhaDePagamento.ImpostoRenda = (salarioBruto * 0.075m) - 142.80m;
    }
    else if (salarioBruto <= 3751.05m)
    {
        folhaDePagamento.ImpostoRenda = (salarioBruto * 0.15m) - 354.80m;
    }
    else if (salarioBruto <= 4664.68m)
    {
        folhaDePagamento.ImpostoRenda = (salarioBruto * 0.225m) - 636.13m;
    }
    else
    {
        folhaDePagamento.ImpostoRenda = (salarioBruto * 0.275m) - 869.36m;
    }

    // Cálculo do INSS
    if (salarioBruto <= 1693.72m)
    {
        folhaDePagamento.INSS = salarioBruto * 0.08m;
    }
    else if (salarioBruto <= 2822.90m)
    {
        folhaDePagamento.INSS = salarioBruto * 0.09m;
    }
    else if (salarioBruto <= 5645.80m)
    {
        folhaDePagamento.INSS = salarioBruto * 0.11m;
    }
    else
    {
        folhaDePagamento.INSS = 621.03m;
    }

    // Cálculo do FGTS
    folhaDePagamento.FGTS = folhaDePagamento.SalarioBruto * 0.08m;

    // Cálculo do Salário Líquido
    folhaDePagamento.SalarioLiquido = folhaDePagamento.SalarioBruto - folhaDePagamento.ImpostoRenda - folhaDePagamento.INSS;

    _ctx.FolhasDePagamento.Add(folhaDePagamento);
        _ctx.SaveChanges();

    return CreatedAtAction("GetFolhaDePagamento", new { id = folhaDePagamento.FolhaId }, folhaDePagamento);
    }

    // GET: api/FolhaDePagamento
    [HttpGet]
    public ActionResult<IEnumerable<FolhaDePagamento>> ListarFolhasDePagamento()
    {
        var folhasDePagamento = _ctx.FolhasDePagamento.Include(f => f.Funcionario).ToList();
        return Ok(folhasDePagamento);
    }

    // GET: api/FolhaDePagamento/5
    [HttpGet("{id}")]
    public ActionResult<FolhaDePagamento> GetFolhaDePagamento(int id)
    {
        var folhaDePagamento = _ctx.FolhasDePagamento
            .Include(f => f.Funcionario)
            .FirstOrDefault(f => f.FolhaId == id);

        if (folhaDePagamento == null)
        {
            return NotFound(); // Retorna 404 se a folha de pagamento não for encontrada
        }

        return folhaDePagamento;
    }

    // GET: api/FolhaDePagamento/BuscarPorFuncionario/5
    [HttpGet("BuscarPorFuncionario/{funcionarioId}")]
    public ActionResult<IEnumerable<FolhaDePagamento>> BuscarPorFuncionario(int funcionarioId)
    {
        var folhasDePagamento = _ctx.FolhasDePagamento
            .Include(f => f.Funcionario)
            .Where(f => f.FuncionarioId == funcionarioId)
            .ToList();

        if (!folhasDePagamento.Any())
        {
            return NotFound(); // Retorna 404 se não forem encontradas folhas de pagamento para o funcionário
        }

        return Ok(folhasDePagamento);
    }
}
