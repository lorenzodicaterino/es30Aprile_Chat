using Esercitazione30AprileC_.dtos;
using Esercitazione30AprileC_.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

        public bool IsUsernameInToken(string jwtToken, string username)
        {
            // Verifica che il token non sia vuoto
            if (string.IsNullOrEmpty(jwtToken))
            {
                throw new ArgumentException("Il token JWT non può essere vuoto o nullo.", nameof(jwtToken));
            }

            // Verifica che il nome utente non sia vuoto
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Il nome utente non può essere vuoto o nullo.", nameof(username));
            }

            try
            {
                // Decodifica il token JWT
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.ReadJwtToken(jwtToken);

                // Cerca il claim Sub che corrisponde al nome utente
                Claim claim = token.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub && c.Value == username);

                // Se il claim Sub è presente con lo stesso valore del nome utente, restituisce true, altrimenti false
                return claim != null;
            }
            catch (Exception ex)
            {
                // Gestione degli errori
                Console.WriteLine($"Si è verificato un errore durante la decodifica del token JWT: {ex.Message}");
                return false;
            }
        }
    }
}
