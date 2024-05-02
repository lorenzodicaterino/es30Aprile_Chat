namespace Esercitazione30AprileBackEnd.Repository
{
    public interface IRepository<T>
    {
        bool Insert(T t);
        bool Update(T t);
        bool Delete(T t);
        List<T> GetAll();
        
    }
}
