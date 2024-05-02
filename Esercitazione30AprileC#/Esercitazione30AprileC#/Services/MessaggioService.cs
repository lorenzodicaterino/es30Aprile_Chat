using Esercitazione30AprileC_.dtos;
using Esercitazione30AprileC_.Models;
using Esercitazione30AprileC_.Repository;
using MongoDB.Bson;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Esercitazione30AprileC_.Services
{
    public class MessaggioService
    {
        private readonly MessaggioRepository repo;
        private readonly UtenteService utente;
        private readonly RoomService room;

        public MessaggioService(MessaggioRepository repo, UtenteService utente, RoomService room)
        {  
            this.repo = repo;
            this.utente = utente;
            this.room = room;
        }

        public bool InserisciMessaggio(string stanza, MessaggioDTO mex)
        {
            Utente u = utente.GetByNome(mex.NomUte);
            Room r = room.GetByNome(stanza);

            if(u is not null && r is not null && room.UtenteContenuto(r.Nome,u.Username))
            {
                return repo.Insert(new Messaggio()
                    {
                        NomeUtente = mex.NomUte,
                        Stanza = r.RoomID,
                        Contenuto = mex.Con,
                        Orario = DateTime.Now
                    });
            }

            return false;
        }

        public bool AssegnaMessaggio()
        {
            foreach (Messaggio m in repo.GetAll()) 
            {
                foreach(RoomDTO r in room.GetAll())
                {
                    if (room.GetByNome(r.Nom).RoomID == m.Stanza && !room.GetByNome(r.Nom).Messaggi.Contains(m.MessaggioID))
                    {
                        r.Mes.Add(m.MessaggioID);
                        room.ModificaRoom(r);
                    }
                }
            }

            return true;
        }

        public List<MessaggioDTO> RecuperaTuttiPerRoom (string r)
        {
            Room temp = room.GetByNome(r);

            return repo.GetAllByRoom(temp).Select(m=> this.ConvertiMsgDTO(m)).ToList();
        }

        public MessaggioDTO ConvertiMsgDTO(Messaggio temp)
        {
            return new MessaggioDTO()
            {
                NomUte = temp.NomeUtente,
                Con = temp.Contenuto,
                Sta = temp.Stanza,
                Ora = temp.Orario
            };
        }

        public MessaggioDTO GetByObjectId (ObjectId mex)
        {
            return this.ConvertiMsgDTO(repo.GetByObjectId(mex));
        }
    }
}
