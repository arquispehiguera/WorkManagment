using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkManagment.Aplication.Services;
using WorkManagment.Core.Entities;

namespace WorkManagment.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        public ActionResult Index()
        {
            TempData["Nombres"] = ViewBag.Message = HttpContext.Session.GetString("Nombres");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetUsers()
        {
           var User = await _userService.PopulateUsuario();
           return Ok(User);
        }

        [HttpPost]
        public async Task<IActionResult> GetAreas()
        {
            var User = await _userService.PopulateArea();
            return Ok(User);
        }

        [HttpPost]
        public async Task<IActionResult> InsertUsuario([FromBody]  Usuario ent)
        {
            try
            {
                var User = await _userService.InsertUsuario(ent);
                return Ok(User);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUsuario([FromBody] Usuario ent)
        {
            try
            {
                var User = await _userService.DeleteUsuario(ent);
                return Ok(User);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
