using ProjetoMei.Back.Models;

namespace ProjetoMei.Back.Repository
{
    public interface IAdministradorRepository
    {
        AdministradorModel? BuscarPorEmail(string email);
        bool ExisteEmail(string email);
    }
}