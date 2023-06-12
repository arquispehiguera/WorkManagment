using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkManagment.Aplication.Services;
using WorkManagment.Core.Entities;

namespace WorkManagment.Web.Controllers
{
    public class LoguinController : Controller
    {
        private readonly UserService _userService;
        public LoguinController(UserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] Usuario ent)
        {
            try
            {
                string authenticationResult = await _userService.Authenticate(ent.Dni, ent.Password);

                if (authenticationResult == "Autenticado")
                {
                    IReadOnlyList<Usuario> user = await _userService.PopulateUsuarioByDni(ent.Dni);
                    HttpContext.Session.SetInt32("Perfil", user[0].IdArea);
                    HttpContext.Session.SetInt32("Dni", user[0].Dni);
                    HttpContext.Session.SetString("Nombres", String.Concat(user[0].Nombres," ", user[0].Apellidos));
                    return Ok(authenticationResult);
                }
                else
                {
                    return Ok(authenticationResult);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
