using InfoDengue.Api.Request;
using InfoDengue.Services.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfoDengue.Api.Controllers
{
    public class LoginController : ControllerBase
    {
        //atributo
        private readonly JwtService _jwtService;

        //construtor para injeção de dependência
        public LoginController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost]
        public IActionResult Post(LoginRequest request)
        {
            if (request.Email.Equals("lucassilva@fiotec.fiocruz.br") && request.Senha.Equals("15379862799"))
                return StatusCode(200, _jwtService.GenerateToken("admin"));
            else
                return StatusCode(401); 
        }
    }
}