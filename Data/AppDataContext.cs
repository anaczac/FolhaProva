using Microsoft.EntityFrameworkCore;
using Folha.Models;

namespace Folha.Data;

public class AppDataContext : DbContext
{
    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) { }

    //Classes que vão virar tabelas no banco de dados
    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<FolhaDePagamento> FolhasDePagamento { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);
    }
}
