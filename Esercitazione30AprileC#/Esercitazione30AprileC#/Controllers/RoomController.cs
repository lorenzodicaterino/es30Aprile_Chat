using Esercitazione30AprileC_.dtos;
using Esercitazione30AprileC_.Services;
using Microsoft.AspNetCore.Mvc;
using Esercitazione30AprileC_.Util;

namespace Esercitazione30AprileC_.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : Controller
    {
        private readonly RoomService service;

        public RoomController(RoomService service)
        {
            this.service = service;
        }
       
        [HttpPost]
        public IActionResult Inserisci(RoomDTO dto)
        {
            if (service.Inserisci(dto))
                return Ok(new Risposta()
                {
                    Status = "SUCCESS"
                });
            else
                return Ok(new Risposta()
                {
                    Status = "ERROR"
                });
        }

        [HttpPost("aggiungi_partecipante")]
        public IActionResult AggiungiPartecipante(string stanza, string utente)
        {
            if (service.AggiungiPartecipante(stanza, utente))
                return Ok(new Risposta()
                {
                    Status = "SUCCESS"
                });
            else
                return Ok(new Risposta()
                {
                    Status = "ERRROR"
                });
        }

        [HttpGet("dettaglio/{codice}")]
        public IActionResult DettaglioGruppo(string codice)
        {
            return Ok(new Risposta()
            {
                Status = "SUCCESS",
                Data = service.RestituisciDettaglio(codice)
            });
        }

        [HttpDelete("{codice}")]
        public IActionResult EliminaStanza(string codice)
        {
            return Ok(service.EliminaStanza(codice));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new Risposta()
            {
                Status = "SUCCESS",
                Data = service.GetAll()
            });
        }

        [HttpGet("global")]
        public IActionResult CreaGlobal()
        {
            service.CreaGlobal();
            return Ok(new Risposta()
            {
                Status="SUCCESS"
            });
        }
    }
}
