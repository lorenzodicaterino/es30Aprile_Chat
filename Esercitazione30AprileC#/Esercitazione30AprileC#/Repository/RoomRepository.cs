using Esercitazione30AprileC_.Models;
using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Esercitazione30AprileC_.Repository
{
    public class RoomRepository : IRepository<Room>
    {

        private IMongoCollection<Room> room;
        private readonly ILogger _logger;
        public RoomRepository(IConfiguration config, ILogger<MessaggioRepository> logger)
        {
            this._logger = logger;
            string? connessioneLocale = config.GetValue<string>("MongoDbSettings:Locale");
            string? databaseName = config.GetValue<string>("MongoDbSettings:DatabaseName");

            MongoClient client = new MongoClient(connessioneLocale);
            IMongoDatabase _database = client.GetDatabase(databaseName);
            this.room = _database.GetCollection<Room>("Rooms");
        }
        public bool Delete(Room t)
        {
            t.Deleted = DateTime.Now;
            return this.Update(t);
        }

        public List<Room> GetAll()
        {
            return room.Find(o => o.Deleted == null).ToList();
        }

        public Room? GetByNome(string? nome)
        {
            int i = room.Find(r=>r.Nome==nome && r.Deleted == null).ToList().Count;

            if (i > 0)
                return room.Find(r => r.Nome == nome && r.Deleted == null).ToList()[0];
            else
                return null;
        }

        public bool Insert(Room item)
        {
            try
            {
                if (room.Find(i => i.Nome == item.Nome).ToList().Count > 0)
                    return false;

                room.InsertOne(item);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Update(Room item)
        {
            Room? temp = this.GetByNome(item.Nome);

            if (temp != null)
            {
                temp.Nome = item.Nome != null ? item.Nome : temp.Nome;
                temp.Descrizione = item.Descrizione != null ? item.Descrizione : temp.Descrizione;
                temp.Deleted = item.Deleted != null ? item.Deleted : temp.Deleted;
                temp.Messaggi = item.Messaggi != null ? item.Messaggi : temp.Messaggi;
                
                foreach(string p in item.Partecipanti)
                {
                    if(!temp.Partecipanti.Contains(p))
                        temp.Partecipanti.Add(p);
                }

                var filter = Builders<Room>.Filter.Eq(i => i.RoomID, temp.RoomID);
                try
                {
                    room.ReplaceOne(filter, temp);
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return false;
                }
            }

            return false;
        }

        public bool AggiungiPartecipante(Room r,Utente t)
        {
            try
            {
                Room? temp = this.GetByNome(r.Nome);
                temp.Partecipanti.Add(t.Username);
                this.Update(temp);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public Room? GetByObjectId(ObjectId id)
        {
            return room.Find(r => r.RoomID == id).ToList()[0];
        }

        public bool CreateGlobal()
        {
            return this.Insert(new Room()
            {
                Nome = "Global",
                Descrizione = "Global chat.",
                Creatore = "Johnny Pax",
                DataCreazione = DateOnly.MinValue,
                Deleted = null
            });
        }

    }
}

