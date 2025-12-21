using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using ProjetoMei.Model;
using Microsoft.EntityFrameworkCore;

namespace ProjetoMei.Data
{
    public class MeiDbContext : DbContext
    {
        public MeiDbContext(DbContextOptions options) : base(options)
        {
        
        }

        public DbSet<UsuarioMeiModel> Meis {get; set;}
        
    }
}