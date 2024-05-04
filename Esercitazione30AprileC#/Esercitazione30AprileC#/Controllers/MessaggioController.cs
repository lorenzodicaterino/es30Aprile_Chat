using Esercitazione30AprileC_.dtos;
using Esercitazione30AprileC_.Services;
using Microsoft.AspNetCore.Mvc;
using Esercitazione30AprileC_.Util;
using MongoDB.Driver.Core.Operations;

namespace Esercitazione30AprileC_.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessaggioController : Controller
    {
        private readonly MessaggioService service;
        
        public MessaggioController(MessaggioService service)
        {
            this.service = service;
        }

        [HttpPost]
        public IActionResult InserisciMessaggio(MessaggioDTO mex)
        {
            if (service.InserisciMessaggio(mex))
                return Ok(service.AssegnaMessaggio());
            else
                return Ok(new Risposta()
                {
                    Status = "ERROR"
                });
        }

        [HttpGet("{stanza}")]
        public IActionResult RecuperaPerStanza(string stanza)
        {
            return Ok(new Risposta()
            {
                Status = "SUCCESS",
                Data = service.RecuperaTuttiPerRoom(stanza)
            });
        }
    }
}
