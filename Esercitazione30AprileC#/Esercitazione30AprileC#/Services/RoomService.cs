using Esercitazione30AprileC_.dtos;
using Esercitazione30AprileC_.Repository;
using Esercitazione30AprileC_.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using MongoDB.Bson;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Esercitazione30AprileC_.Services
{
    public class RoomService
    {
        private readonly RoomRepository repo;
        private readonly UtenteRepository utente;
        private readonly MessaggioRepository messaggio;

        public RoomService(MessaggioRepository messaggio, RoomRepository repo, UtenteRepository utente) 
        {
            this.messaggio = messaggio;
            this.repo = repo;
            this.utente = utente;
        }

        public List<RoomDTO>? NonAppartiene(string username)
        {
            List<Room> rooms = repo.GetAll();
            
            List<RoomDTO> dtos = new List<RoomDTO>();

            foreach(Room r in rooms)
            {
                if (!r.Partecipanti.Contains(username))
                    dtos.Add(new RoomDTO()
                    {
                        Nom = r.Nome,
                        Cre = r.Creatore,
                        Dat = r.DataCreazione,
                        Des = r.Descrizione,
                        Mes = r.Messaggi,
                        Par = r.Partecipanti
                    });
            }
            return dtos;
        }

        

        public bool Inserisci (RoomDTO dto)
        {
            if (repo.GetAll().Where(r => r.Nome.Equals(dto.Nom)).ToList().Count > 0)
                return false;


            return repo.Insert(new Room()
            {
                Nome = dto.Nom,
                Descrizione = dto.Des,
                DataCreazione = DateOnly.FromDateTime(DateTime.Now),
                Partecipanti = new List<string>()
                {
                    new string (dto.Cre)
                },
                Creatore = dto.Cre,
                Deleted = null
            });
        }

        public bool AggiungiPartecipante(string room, string u)
        {
            Room rom = repo.GetByNome(room);
            Utente ute = utente.GetByNome(u);

            if(rom is not null && ute is not null)
            {
                if (rom.Partecipanti.Contains(ute.Username))
                    return false;
                else
                    return repo.AggiungiPartecipante(rom, ute);
            }

            return false;
            
        }

        public Room? GetByNome(string nome)
        {
            return repo.GetByNome(nome);
        }

        public Room? GetByObjectId(ObjectId id)
        {
            return repo.GetByObjectId(id);
        }

        public bool UtenteContenuto(string room, string u)
        {
            Room r = this.GetByNome(room);
            Utente ute = utente.GetByNome(u);

            if (r.Partecipanti.Contains(ute.Username))
                return true;
            else
                return false;
        }

        public RoomDTO? RestituisciDettaglio(string room)
        {
            Room r = repo.GetByNome(room);

            if (r is not null)
                return new RoomDTO()
                {
                    Nom = r.Nome,
                    Des = r.Descrizione,
                    Cre = r.Creatore,
                    Dat = r.DataCreazione,
                    Mes = r.Messaggi,
                    Par = r.Partecipanti
                };
            else 
                return null;
        }

        public bool ModificaRoom(RoomDTO dto)
        {
            return repo.Update(new Room()
            {
                Nome = dto.Nom,
                Messaggi = dto.Mes
            });
        }

        public List<RoomDTO> GetAll()
        {
            return repo.GetAll().Select(r => new RoomDTO()
            {
                Nom = r.Nome,
                Des = r.Descrizione,
                Dat = r.DataCreazione,
                Cre = r.Creatore,
                Mes = r.Messaggi,
                Par = r.Partecipanti
            }).ToList();
        }

        public bool AssegnaCreatore(RoomDTO dto)
        {
            dto.Par.Add(dto.Cre);
            return this.ModificaRoom(dto);
        }

        public bool EliminaStanza(string nome)
        {
            ObjectId codiceRoom = repo.GetByNome(nome).RoomID;

            if(repo.Delete(repo.GetByNome(nome)))
            { 
                foreach (Messaggio m in messaggio.GetAll())
                {
                    if (m.Stanza == codiceRoom)
                        messaggio.Delete(m);
                }
            }

            return true;
        }

        public bool CreaGlobal()
        {
            return repo.CreateGlobal();
        }

        public List<RoomDTO>ListePerUtente(string username)
        {
            return repo.RoomPerUtente(username).Select(r => new RoomDTO()
            {
                Nom = r.Nome,
                Des = r.Descrizione,
                Dat = r.DataCreazione,
                Cre = r.Creatore,
                Mes = null,
                Par = null
            }).ToList();
        }

        public bool AbbandonaGruppo(string utente, string gruppo)
        {
            Room r = repo.GetByNome(gruppo);
            if (repo.RimuoviPartecipante(r, utente))
                return true;
            else
                return false;
        }
    }
}
