using Esercitazione30AprileC_.dtos;
using Esercitazione30AprileC_.Filters;
using Esercitazione30AprileC_.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Esercitazione30AprileC_.Util;

namespace Esercitazione30AprileC_.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UtenteController : Controller
    {
        private readonly UtenteService _serviceutenti;

        public UtenteController(UtenteService service)
        {
            _serviceutenti = service;
        }

        [HttpPost("login")]
        public IActionResult Loggati(UtenteDTO objLogin)
        {
            //TODO: Verifica accesso, emissione JWT
            if (_serviceutenti.LoginUtente(objLogin))
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.Sub, objLogin.Use),
                    new Claim("UserType","USER"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),      //Evito che due dispositivi abbiano lo stesso JWT TOken (rubato)
                    new Claim ("Username", objLogin.Use)
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_super_secret_key_your_super_secret_key"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "TeamPelati",
                    audience: "Utenti",
                    claims: claims,          //Body o Payload del JWT
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds
                    );
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });

            }
            return Unauthorized();
        }

        [HttpPost("register")]
        public IActionResult Register(UtenteDTO obj)
        {
            if (_serviceutenti.RegistraUtente(obj))
                return Ok(new Risposta()
                {
                    Status = "SUCCESS",
                    Data = _serviceutenti.AggiungiAGlobal(obj.Use)
                }) ;
            return BadRequest();
        }

        [HttpGet("profiloutente")]
        [AutorizzazioneUtente("USER")]
        public IActionResult DammiInformazioniUtente()
        {
            var nickname = User.Claims.FirstOrDefault(x => x.Type == "Username")?.Value;
            if(nickname is not null)
            {
                return Ok(new Risposta()
                {
                    Status = "SUCCESS",
                    Data = _serviceutenti.DtoByNome(nickname)
                }); ;
            }

            return BadRequest();
        }


        [HttpDelete]
        public IActionResult Elimina(UtenteDTO utente)
        {
            if (_serviceutenti.EliminaPerNome(utente))
                return Ok(new Risposta()
                {
                    Status="SUCCESS"
                });
            else
                return Ok(new Risposta()
                {
                    Status = "ERROR"
                });
        }

        [HttpGet]
        public IActionResult RecuperaTutti()
        {
            return Ok(new Risposta()
            {
                Status = "SUCCESS",
                Data = _serviceutenti.RestituisciTutto()
            });
        }
    }
}
