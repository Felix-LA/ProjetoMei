using Microsoft.EntityFrameworkCore;
using ProjetoMei.Back.Models;

namespace ProjetoMei.Back.Data
{
    public class MeiDbContext : DbContext
    {
        // A configuração do banco (em memória ou MySQL) fica somente no Program.cs.
        public MeiDbContext(DbContextOptions<MeiDbContext> options) : base(options)
        {
        }

        public DbSet<UsuarioMeiModel> Meis { get; set; }
    }
}
