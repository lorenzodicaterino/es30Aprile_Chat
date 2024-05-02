using MongoDB.Bson;

namespace Esercitazione30AprileC_.Repository
{
    public interface IRepository<T>
    {
        T? GetByObjectId(ObjectId id);
        T? GetByNome(string nome);
        List<T> GetAll();
        bool Insert(T t);
        bool Update(T t);
        bool Delete(T t);
    }
}
