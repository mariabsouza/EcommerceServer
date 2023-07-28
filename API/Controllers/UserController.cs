using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using static BCrypt.Net.BCrypt;
using API.Models;
using API.Context;
using API.Services;
using Microsoft.AspNetCore.Identity;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly EcommerceContext _context;

        public UserController(EcommerceContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(User user)
        {

            if(_context.Users.Where(c => c.Email == user.Email).ToList().Count > 0)
            {
                return Conflict("E-mail já está em uso");
            }

            user.Senha = HashPassword(user.Senha, 12);
            user.Papel = "cliente";
            _context.Users.Add(user);
            _context.SaveChanges();


            var token = TokenService.GenerateToken(user);

            return Ok(token);
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(User user)
        {

            try
            {
                User userDB = _context.Users.Where(c => c.Email == user.Email).Select(c => c).First();
                if (!Verify(user.Senha, userDB.Senha))
                {
                    return Unauthorized("Senha incorreta!");
                }

                var token = TokenService.GenerateToken(userDB);

                return Ok(token);

            } catch (Exception ex)
            {
                return BadRequest("Usuário ou senha inválidos");
            }

        }
    }
}
