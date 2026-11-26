using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjetoMei.Back.Filters
{
    /// <summary>
    /// Protege endpoints que só o próprio site (Front) pode chamar: exige o cabeçalho
    /// X-Api-Key igual a "Api:Chave" do appsettings.json. Quem acessa a API direto pelo
    /// navegador não tem a chave e recebe 401.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ChaveApiAttribute : Attribute, IAuthorizationFilter
    {
        public const string Cabecalho = "X-Api-Key";

        public void OnAuthorization(AuthorizationFilterContext contexto)
        {
            IConfiguration config = contexto.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            string esperada = config["Api:Chave"] ?? string.Empty;
            string recebida = contexto.HttpContext.Request.Headers[Cabecalho].ToString();

            bool valida = esperada.Length > 0 && CryptographicOperations.FixedTimeEquals(
                SHA256.HashData(Encoding.UTF8.GetBytes(recebida)),
                SHA256.HashData(Encoding.UTF8.GetBytes(esperada)));

            if (!valida)
            {
                contexto.Result = new UnauthorizedObjectResult(new { mensagem = "Chave da API ausente ou inválida." });
            }
        }
    }
}