namespace InfoDengue.Api.Requests
{
    public class PerfilPutRequest
    {
        public Guid IdPerfil { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}