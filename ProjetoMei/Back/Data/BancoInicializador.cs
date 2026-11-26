using ProjetoMei.Back.Models;
using ProjetoMei.Back.Services;

namespace ProjetoMei.Back.Data
{
    /// <summary>
    /// Prepara o banco ao iniciar: cria o banco e as tabelas se ainda não existirem
    /// e cadastra o primeiro administrador (dados em "Admin" no appsettings.json).
    /// </summary>
    public static class BancoInicializador
    {
        public static void Inicializar(IServiceProvider servicos, IConfiguration configuracao)
        {
            MeiDbContext db = servicos.GetRequiredService<MeiDbContext>();

            // Não altera tabelas que já existem: se o banco já foi criado (por este método ou
            // pelo script Banco/buscamei.sql), apenas segue em frente.
            db.Database.EnsureCreated();

            CriarPrimeiroAdministrador(db, configuracao);

            // 5 MEIs aprovados por categoria, só para a vitrine não ficar vazia.
            if (configuracao.GetValue("Seed:DadosExemplo", true))
            {
                DadosExemplo.Semear(db);
            }
        }

        private static void CriarPrimeiroAdministrador(MeiDbContext db, IConfiguration configuracao)
        {
            if (db.Administradores.Any())
            {
                return;
            }

            string nome = configuracao["Admin:Nome"] ?? "Administrador";
            string email = (configuracao["Admin:Email"] ?? "admin@buscamei.com").Trim().ToLowerInvariant();
            string senha = configuracao["Admin:Senha"] ?? string.Empty;

            if (senha.Length < 8)
            {
                throw new InvalidOperationException(
                    "Defina Admin:Senha (mínimo 8 caracteres) no appsettings.json para criar o primeiro administrador.");
            }

            db.Administradores.Add(new AdministradorModel
            {
                Nome = nome,
                Email = email,
                SenhaHash = SenhaHasher.Gerar(senha),
                DataCadastro = DateTime.UtcNow
            });
            db.SaveChanges();
        }
    }
}