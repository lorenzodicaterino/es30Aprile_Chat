using Esercitazione30AprileBackEnd.Models;
using MongoDB.Driver;

namespace Esercitazione30AprileBackEnd.Repository
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

        public bool Delete(Messaggio t)
        {
            throw new NotImplementedException();
        }

        public List<Messaggio> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Insert(Messaggio t)
        {
            throw new NotImplementedException();
        }

        public bool Update(Messaggio t)
        {
            throw new NotImplementedException();
        }
    }
}
