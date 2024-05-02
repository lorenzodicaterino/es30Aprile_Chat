using Esercitazione30AprileC_.dtos;
using Esercitazione30AprileC_.Models;
using Esercitazione30AprileC_.Repository;
using System.Security.Cryptography;
using System.Text;

namespace Esercitazione30AprileC_.Services
{
    public class UtenteService
    {
        private readonly UtenteRepository _repo;
        private readonly RoomService room;
        public UtenteService(RoomService room,UtenteRepository repo)
        {
            this.room = room;
            _repo = repo;
        }

        public Utente GetByNome(string username)
        {
            return _repo.GetByNome(username);
        }
        public List<UtenteDTO> RestituisciTutto()
        {
            List<UtenteDTO> elenco = _repo.GetAll().Select(p => new UtenteDTO()
            {
                CodUte = p.CodiceUtente,
                Use = p.Username,
                Pas = p.Passw
            }).ToList();

            return elenco;
        }
        public UtenteDTO ConvertiUtenteDTO(Utente temp)
        {
            return new UtenteDTO()
            {
                CodUte = temp.CodiceUtente,
                Use = temp.Username,
                Pas = temp.Passw
            };
        }
        public bool LoginUtente(UtenteDTO obj)
        {
            obj.Pas = CalculateMD5Hash(obj.Pas);
            return _repo.CheckLogin(obj) is not null ? true : false;
        }
        public bool RegistraUtente(UtenteDTO obj)
        {
            if (obj is not null)
            {
                try
                {
                    Utente temp = new Utente()
                    {
                        CodiceUtente = Guid.NewGuid().ToString().ToUpper(),
                        Username = obj.Use,
                        Passw = CalculateMD5Hash(obj.Pas),
                        Deleted = null
                    };
                    
                    return _repo.Insert(temp);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return false;
        }
        public UtenteDTO DtoByNome(string email)
        {
            Utente? temp = _repo.GetByNome(email);
            if (temp is not null)
            {
                UtenteDTO prodto = new UtenteDTO()
                {
                    CodUte = temp.CodiceUtente,
                    Use = temp.Username,
                    Pas = temp.Passw
                };
                if (prodto is not null)
                {
                    return prodto;
                }
            }
            return new UtenteDTO();
        }
        public bool EliminaPerNome(UtenteDTO? pro)
        {
            Utente? temp = _repo.GetAll().FirstOrDefault(p => p.Username == pro.Use);

            if (temp is not null)
                return _repo.Delete(temp.Username);

            return false;
        }

        public bool AggiornaUtente(UtenteDTO obj)
        {
            if (obj is not null)
            {
                Utente temp = new Utente()
                {
                    CodiceUtente = obj.CodUte,
                    Username = obj.Use,
                    Passw = obj.Pas
                };
                return _repo.Update(temp);
            }
            return false;
        }
        public static string CalculateMD5Hash(string input)
        {

            // Creazione dell'oggetto MD5
            using (MD5 md5 = MD5.Create())
            {
                // Conversione della stringa in un array di byte
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);

                // Calcolo dell'hash MD5
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Converting the byte array to a hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public bool AggiungiAGlobal(string nome)
        {
            return room.AggiungiPartecipante("Global", nome);
        }
    }
}
