using Microsoft.EntityFrameworkCore;
using ProjetoMei.Back.Models;

namespace ProjetoMei.Back.Data
{
    public class MeiDbContext : DbContext
    {
        // A configuração do banco (MySQL) fica somente no Program.cs.
        public MeiDbContext(DbContextOptions<MeiDbContext> options) : base(options)
        {
        }

        public DbSet<UsuarioMeiModel> Meis { get; set; }
        public DbSet<AdministradorModel> Administradores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioMeiModel>(e =>
            {
                e.ToTable("usuarios_mei");
                e.HasIndex(u => u.Cpf).IsUnique();
                e.HasIndex(u => u.Email).IsUnique();
                e.HasIndex(u => u.Status);
            });

            modelBuilder.Entity<AdministradorModel>(e =>
            {
                e.ToTable("administradores");
                e.HasIndex(a => a.Email).IsUnique();
            });
        }
    }
}