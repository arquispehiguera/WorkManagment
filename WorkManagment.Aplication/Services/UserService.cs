using System.Collections.Generic;
using System.Threading.Tasks;
using WorkManagment.Core.Entities;
using WorkManagment.Core.Interfaces;

namespace WorkManagment.Aplication.Services
{
    public  class UserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> InsertUsuario(Usuario ent)
        {
            return await _userRepository.InsertUsuario(ent);
        }
        public async Task<bool> DeleteUsuario(Usuario ent)
        {
            return await _userRepository.DeleteUsuario(ent);
        }
        public async Task<IReadOnlyList<Area>> PopulateArea()
        {
            return await _userRepository.PopulateArea();
        }

        public async Task<IReadOnlyList<Usuario>> PopulateUsuario()
        {
            return await _userRepository.PopulateUsuario();
        }
        public async Task<string> DecryptFromBase64(string base64String)
        {
            return await _userRepository.DecryptFromBase64(base64String);
        }
        public async Task<string> Authenticate(int Dni, string Password)
        {
            return await _userRepository.Authenticate(Dni,Password);
        }
        public async Task<IReadOnlyList<Usuario>> PopulateUsuarioByDni(int Dni)
        {
            return await _userRepository.PopulateUsuarioByDni(Dni);
        }
    }
}
