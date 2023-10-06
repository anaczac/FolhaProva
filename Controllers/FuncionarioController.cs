using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Folha.Data;
using Folha.DTOs;
using Folha.Models;

namespace Folha.Controllers;

[ApiController]
[Route("api/Funcionario")]
public class FuncionarioController : ControllerBase
{
    private readonly AppDataContext _ctx;
    public FuncionarioController(AppDataContext context)
    {
        _ctx = context;
    } 

   
    [HttpGet]
    [Route("listar")]
    public IActionResult Listar()
    {
        List<Funcionario> funcionarios = _ctx.Funcionarios.ToList();
        return funcionarios.Count == 0 ? NotFound() : Ok(funcionarios);
    }

[HttpPost]
[Route("cadastrar")]
public IActionResult Cadastrar([FromBody] FuncionarioDTO funcionarioDTO)
{
    try
    {
       Funcionario funcionario = new Funcionario
        {
            Nome = funcionarioDTO.Nome,
            Cpf = funcionarioDTO.Cpf,
        };

        _ctx.Funcionarios.Add(funcionario);
        _ctx.SaveChanges();

        return Created("",funcionario);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return BadRequest(e.Message);
    }
}

    [HttpGet]
    [Route("buscar/{nome}")]
    public IActionResult Buscar([FromRoute] string nome)
    {
        //Expressão lambda para buscar um registro na base de dados com EF
        foreach (Funcionario funcionarioCadatrado in _ctx.Funcionarios.ToList())
        {
            if (funcionarioCadatrado.Nome == nome)
            {
                return Ok(funcionarioCadatrado);
            }
        }
        return NotFound();
    }

    [HttpDelete]
    [Route("deletar/{id}")]
    public IActionResult Deletar([FromRoute] int id)
    {
        try
        {
            Funcionario? funcionarioCadatrado = _ctx.Funcionarios.Find(id);
            if (funcionarioCadatrado != null)
            {
                _ctx.Funcionarios.Remove(funcionarioCadatrado);
                _ctx.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("alterar/{id}")]
    public IActionResult Alterar([FromRoute] int id,
        [FromBody] Funcionario funcionario)
    {
        try
        {
            //Expressões lambda
            Funcionario? funcionarioCadatrado =
                _ctx.Funcionarios.FirstOrDefault(x => x.FuncionarioId == id);
            if (funcionarioCadatrado != null)
            {
                funcionarioCadatrado.Nome = funcionario.Nome;
                funcionarioCadatrado.Cpf = funcionario.Cpf;
                _ctx.Funcionarios.Update(funcionarioCadatrado);
                _ctx.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
