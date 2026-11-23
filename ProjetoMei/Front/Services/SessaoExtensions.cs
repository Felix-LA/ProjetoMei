using System.Text.Json;
using ProjetoMei.Front.Models;

namespace ProjetoMei.Front.Services
{
    /// <summary>Guarda e lê o usuário logado na sessão (usuario_logado() do PHP).</summary>
    public static class SessaoExtensions
    {
        private const string Chave = "usuario";

        public static SessaoUsuario? UsuarioLogado(this HttpContext contexto)
        {
            string? json = contexto.Session.GetString(Chave);
            if (string.IsNullOrEmpty(json)) return null;

            try { return JsonSerializer.Deserialize<SessaoUsuario>(json); }
            catch (JsonException) { return null; }
        }

        public static void EntrarNaSessao(this HttpContext contexto, SessaoUsuario usuario)
        {
            // Limpa a sessão antes de gravar: o cookie da sessão antiga não serve de ponte.
            contexto.Session.Clear();
            contexto.Session.SetString(Chave, JsonSerializer.Serialize(usuario));
        }

        public static void SairDaSessao(this HttpContext contexto)
        {
            contexto.Session.Clear();
        }
    }
}
