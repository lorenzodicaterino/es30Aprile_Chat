using Esercitazione30AprileBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace Esercitazione30AprileBackEnd.Repository
{
    public class UtenteRepository : IRepository<Utente>
    {
        private readonly Esercitazione30AprileContext ctx;
        public UtenteRepository (Esercitazione30AprileContext ctx)
        {
            this.ctx = ctx;
        }
        public bool Delete(Utente t)
        {
            try
            {

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<Utente> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Insert(Utente t)
        {
            throw new NotImplementedException();
        }

        public bool Update(Utente t)
        {
            try
            {
                Utente temp = this.GetByNome(t.Username);

                if(temp is not null)
                {
                    temp.Passw = t.Passw is not null ? t.Passw : temp.Passw;

                    ctx.Entry(temp).CurrentValues.SetValues(t);
                    ctx.SaveChanges();
                    return true;
                }

                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public Utente GetByNome(string nome)
        {
            return ctx.Utentes.FirstOrDefault(u=>u.Username.Equals(nome));
        }

        public Utente? CheckLogin(Utente obj)
        {
            Utente? temp = ctx.Utentes.FirstOrDefault(p => p.Username == obj.Username && p.Passw == obj.Passw && p.Deleted == null);
            if (temp is not null)
            {
                return temp;
            }
            return temp;
        }
    }
}
