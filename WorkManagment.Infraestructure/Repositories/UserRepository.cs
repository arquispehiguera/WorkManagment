using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WorkManagment.Core.Entities;
using WorkManagment.Core.Exceptions;
using WorkManagment.Core.Interfaces;
using WorkManagment.Infraestructure.Data;

namespace WorkManagment.Infraestructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _dbContext;
        private readonly ILogger<UserRepository> _logger;
        private readonly IConfiguration _configuration;
        public UserRepository(DbContext dbContext, ILogger<UserRepository> logger, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<bool> InsertUsuario(Usuario ent)
        {
            try
            {
                var sql = "INSERT INTO dbo.Usuario (Dni, IdArea, Nombres, Apellidos, Email, Password) " +
                          "VALUES (@Dni, @IdArea, @Nombres, @Apellidos, @Email, @Password)";
                var parameters = new DynamicParameters();
                parameters.Add("@Dni", ent.Dni, DbType.Int32);
                parameters.Add("@IdArea", ent.IdArea, DbType.Int32);
                parameters.Add("@Nombres", ent.Nombres, DbType.String);
                parameters.Add("@Apellidos", ent.Apellidos, DbType.String);
                parameters.Add("@Email", ent.Email, DbType.String);
                parameters.Add("@Password", ent.Password, DbType.String);
                using (var connection = _dbContext.CreateConnection())
                {
                    connection.Open();
                    var result = await connection.ExecuteAsync(sql, parameters);
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al insertar usuario: " + ex.Message);
                throw new BussinessExceptions("Error Interno");
            }
        }
        public async Task<bool> DeleteUsuario(Usuario ent)
        {
            try
            {
                var sql = "delete from dbo.Usuario WHERE Dni = @Dni";
                var parameters = new DynamicParameters();
                parameters.Add("@Dni", ent.Dni, DbType.Int32);
                using (var connection = _dbContext.CreateConnection())
                {
                    connection.Open();
                    var result = await connection.ExecuteAsync(sql, parameters);
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar usuario: " + ex.Message);
                throw new BussinessExceptions("Error Interno");
            }
        }
        public async Task<IReadOnlyList<Area>> PopulateArea()
        {
            try
            {
                var sql = "select*from dbo.Area";
                var parameters = new DynamicParameters();
                using (var connection = _dbContext.CreateConnection())
                {
                    connection.Open();
                    var result = await connection.QueryAsync<Area>(sql, parameters, commandType: CommandType.Text);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al listar area: " + ex.Message);
                throw new BussinessExceptions("Error Interno");
            }
        }
        public async Task<IReadOnlyList<Usuario>> PopulateUsuario()
        {
            try
            {
                var sql = "select*from dbo.Usuario";
                var parameters = new DynamicParameters();
                using (var connection = _dbContext.CreateConnection())
                {
                    connection.Open();
                    var result = await connection.QueryAsync<Usuario>(sql, parameters, commandType: CommandType.Text);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al listar usuarios: " + ex.Message);
                throw new BussinessExceptions("Error Interno");
            }
        }
        public async Task<string> DecryptFromBase64(string base64String)
        {
            string secretKey = "clave-secreta";
            byte[] encryptedBytes = Convert.FromBase64String(base64String);
            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes keyDerivation = new Rfc2898DeriveBytes(secretKey, salt: new byte[0]);
                aes.Key = keyDerivation.GetBytes(aes.KeySize / 8);
                aes.IV = keyDerivation.GetBytes(aes.BlockSize / 8);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        await cryptoStream.WriteAsync(encryptedBytes, 0, encryptedBytes.Length);
                        await cryptoStream.FlushFinalBlockAsync();
                    }

                    byte[] decryptedBytes = memoryStream.ToArray();
                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
        }
        public async Task<string> Authenticate(int Dni, string Password)
        {
            try
            {
                var sql = "Spp_AuthenticateUser";
                var parameters = new DynamicParameters();
                parameters.Add("@Dni", Dni, DbType.Int32);
                parameters.Add("@Password", Password, DbType.String);
                using (var connection = _dbContext.CreateConnection())
                {
                    connection.Open();
                    await Task.Delay(2000);
                    var result = await connection.QueryFirstOrDefaultAsync<string>(sql, parameters, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al consultar usuario: " + ex.Message);
                throw new BussinessExceptions("Error Interno");
            }
        }
        public async Task<IReadOnlyList<Usuario>> PopulateUsuarioByDni(int Dni)
        {
            try
            {
                var sql = "select*from dbo.Usuario where Dni= @Dni";
                var parameters = new DynamicParameters();
                parameters.Add("@Dni", Dni, DbType.Int32);
                using (var connection = _dbContext.CreateConnection())
                {
                    connection.Open();
                    var result = await connection.QueryAsync<Usuario>(sql, parameters, commandType: CommandType.Text);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al listar usuarios: " + ex.Message);
                throw new BussinessExceptions("Error Interno");
            }
        }
    }
}
