using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Esercitazione30AprileC_.Filters
{
    public class AutorizzazioneUtente : Attribute, IAuthorizationFilter
    {
        private readonly string _utenteRichiesta;

        public AutorizzazioneUtente(string tipoUtente)
        {
            _utenteRichiesta = tipoUtente;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var claims = context.HttpContext.User.Claims;
            var userType = claims.FirstOrDefault(c => c.Type == "UserType")?.Value;

            if (userType == null || userType != _utenteRichiesta)
            {
                context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden);
            }
        }
    }
}
