namespace InfoDengue.Services.Authorization
{
    /// <summary>
    /// Modelo de dados do conteudo do TOKEN
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// Chave secreta antifalsificação do token
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// Tempo de expiração em minutos
        /// </summary>
        public int ExpirationInMinutes { get; set; }
    }
}
