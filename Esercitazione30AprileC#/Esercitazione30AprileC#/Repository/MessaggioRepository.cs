using Esercitazione30AprileC_.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Esercitazione30AprileC_.Repository
{
    public class MessaggioRepository : IRepository<Messaggio>
    {
        private IMongoCollection<Messaggio> messaggio;
        private readonly ILogger _logger;
        public MessaggioRepository(IConfiguration config, ILogger<MessaggioRepository> logger)
        {
            this._logger = logger;
            string? connessioneLocale = config.GetValue<string>("MongoDbSettings:Locale");
            string? databaseName = config.GetValue<string>("MongoDbSettings:DatabaseName");

            MongoClient client = new MongoClient(connessioneLocale);
            IMongoDatabase _database = client.GetDatabase(databaseName);
            this.messaggio = _database.GetCollection<Messaggio>("Messaggios");
        }

        public bool Delete(Messaggio item)
        {
            try
            {
                this.messaggio.DeleteOne(i => i.MessaggioID == item.MessaggioID);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }

        }

        public List<Messaggio> GetAll()
        {
            return messaggio.Find(p => true).ToList();
        }

        public List<Messaggio> GetAllByRoom(Room t)
        {
            return messaggio.Find(p => true && p.Stanza == t.RoomID).ToList();
        }

        public bool Insert(Messaggio item)
        {
            try
            {
                messaggio.InsertOne(item);
                _logger.LogInformation("Inserimento effettuato");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return false;
        }


        //TODO
        public bool Update(Messaggio item)
        {
            throw new NotImplementedException();
        }

        public Messaggio? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Messaggio? GetByNome(string nome)
        {
            throw new NotImplementedException();
        }

        public Messaggio? GetByObjectId(ObjectId id)
        {
            throw new NotImplementedException();
        }
    }
}
