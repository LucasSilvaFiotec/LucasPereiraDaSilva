using System.ComponentModel.DataAnnotations;

namespace InfoDengue.Api.Request
{
    public class LoginRequest
    {
        [EmailAddress(ErrorMessage = "Informe um endereço de email válido.")]
        [Required(ErrorMessage = "Informe o email de acesso.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a senha de acesso.")]
        public string Senha { get; set; }
    }
}
