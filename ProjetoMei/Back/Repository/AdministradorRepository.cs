using Microsoft.EntityFrameworkCore;
using ProjetoMei.Back.Data;
using ProjetoMei.Back.Models;

namespace ProjetoMei.Back.Repository
{
    public class AdministradorRepository : IAdministradorRepository
    {
        private readonly MeiDbContext _meiDbContext;

        public AdministradorRepository(MeiDbContext meiDbContext)
        {
            _meiDbContext = meiDbContext;
        }

        public AdministradorModel? BuscarPorEmail(string email)
        {
            return _meiDbContext.Administradores
                .AsNoTracking()
                .FirstOrDefault(a => a.Email == email);
        }

        public bool ExisteEmail(string email)
        {
            return _meiDbContext.Administradores
                .AsNoTracking()
                .Any(a => a.Email == email);
        }
    }
}