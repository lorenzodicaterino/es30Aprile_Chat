using Esercitazione30AprileC_.dtos;
using Esercitazione30AprileC_.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ZstdSharp.Unsafe;

namespace Esercitazione30AprileC_.Repository
{
    public class UtenteRepository : IRepository<Utente>
    {
        private readonly Esercitazione30AprileContext _context;
        public UtenteRepository(Esercitazione30AprileContext ctx)
        {
            _context = ctx;
        }

        public bool Insert(Utente entity)
        {
            try
            {
                _context.Utentes.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public bool Delete(string nome)
        {
            try
            {
                Utente? temp = this.GetByNome(nome);
                if (temp is not null)
                {
                    temp.Deleted = DateTime.Now;
                    this.Update(temp);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public Utente? GetById(int id)
        {
            return _context.Utentes.FirstOrDefault(u=> u.UtenteId == id && u.Deleted == null);
        }
        public Utente? GetByNome(string code)
        {
            return _context.Utentes.FirstOrDefault(p => p.Username == code && p.Deleted == null);
        }

        public List<Utente> GetAll()
        {
            return _context.Utentes.Where(u=>u.Deleted==null).ToList();
        }

        public bool Update(Utente entity)
        {
            try
            {
                _context.Utentes.Update(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public Utente? CheckLogin(UtenteDTO obj)
        {
            Utente? temp = _context.Utentes.FirstOrDefault(p => p.Username == obj.Use && p.Passw == obj.Pas && p.Deleted == null);
            if (temp is not null)
            {
                return temp;
            }
            return temp;
        }

        public bool Delete(Utente t)
        {
            throw new NotImplementedException();
        }

        public Utente? GetByObjectId(ObjectId id)
        {
            throw new NotImplementedException();
        }
    }
}
