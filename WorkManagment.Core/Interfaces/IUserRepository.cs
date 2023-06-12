using System.Collections.Generic;
using System.Threading.Tasks;
using WorkManagment.Core.Entities;

namespace WorkManagment.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> InsertUsuario(Usuario ent);
        Task<bool> DeleteUsuario(Usuario ent);
        Task<IReadOnlyList<Area>> PopulateArea();
        Task<IReadOnlyList<Usuario>> PopulateUsuario();
        Task<string> DecryptFromBase64(string base64String);
        Task<string> Authenticate(int Dni, string Password);
        Task<IReadOnlyList<Usuario>> PopulateUsuarioByDni(int Dni);
    }
}
