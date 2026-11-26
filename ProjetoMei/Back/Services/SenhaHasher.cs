using System.Security.Cryptography;

namespace ProjetoMei.Back.Services
{
    /// <summary>Hash de senha com PBKDF2. Formato guardado: v1.{iterações}.{salt base64}.{hash base64}</summary>
    public static class SenhaHasher
    {
        private const int Iteracoes = 100_000;

        public static string Gerar(string senha)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(senha, salt, Iteracoes, HashAlgorithmName.SHA256, 32);

            return $"v1.{Iteracoes}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        public static bool Verificar(string senha, string guardado)
        {
            string[] partes = (guardado ?? string.Empty).Split('.');
            if (partes.Length != 4 || partes[0] != "v1" || !int.TryParse(partes[1], out int iteracoes))
            {
                return false;
            }

            try
            {
                byte[] salt = Convert.FromBase64String(partes[2]);
                byte[] esperado = Convert.FromBase64String(partes[3]);
                byte[] calculado = Rfc2898DeriveBytes.Pbkdf2(senha, salt, iteracoes, HashAlgorithmName.SHA256, esperado.Length);

                return CryptographicOperations.FixedTimeEquals(calculado, esperado);
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}