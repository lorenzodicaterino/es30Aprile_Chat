using Esercitazione30AprileBackEnd.Models;
using MongoDB.Driver;

namespace Esercitazione30AprileBackEnd.Repository
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
            return room.Find(r => r.Deleted == null).ToList();
        }
        public Room GetByNome(string nome)
        {
            return room.Find(r => r.Deleted == null && r.Nome.Equals(nome)).ToList()[0];
        }
        public bool Insert(Room t)
        {
            try
            {
                if (room.Find(i => i.Nome == t.Nome).ToList().Count > 0)
                    return false;

                t.Partecipanti.Add(t.Creatore);
                room.InsertOne(t);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool Update(Room t)
        {
            Room? temp = GetByNome(t.Nome);

            if (temp != null)
            {
                temp.Nome = t.Nome != null ? t.Nome : temp.Nome;
                temp.Descrizione = t.Descrizione != null ? t.Descrizione : temp.Descrizione;
                temp.Deleted = t.Deleted != null ? t.Deleted : temp.Deleted;
                temp.Partecipanti = t.Partecipanti != null ? t.Partecipanti : temp.Partecipanti;

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
    }
}
