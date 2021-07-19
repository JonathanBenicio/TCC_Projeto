
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Services;

namespace WebApi.Controllers.MySQL
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {

        private readonly WebApiContextMySQL _context;

        public LoginsController(WebApiContextMySQL context)
        {
            _context = context;
        }
        // GET: api/<LoginController>
        [HttpGet]
        /*        [Authorize]*/
        public bool Get()
        {
            return true;
        }

        [HttpPost]
        [Route("nutri")]
        [AllowAnonymous]
        public async Task<ActionResult> Authenticate([FromBody] Login login)
        {
            try
            {
                login.Senha = Hash.Criar(login.Senha);
                if (!_context.Nutricionistas.Any(x => x.Login.Email == login.Email.ToLower() && x.Login.Senha == login.Senha)) return NotFound(new { message = "Email ou Senha Invalido" });


                var user = await _context.Nutricionistas.Include(x => x.Login).FirstAsync(x => x.Login.Senha == login.Senha && x.Login.Email == login.Email);
                /*

                                if (user == null)
                                    return NotFound(new { message = "Email ou Senha Invalido" });*/

                /*if (user.Login.Senha != login.Senha)
                    return NotFound(new { message = "Email ou Senha Invalido" });*/

                var token = TokenService.GerenateToken(user.Login);

                user.Login.Senha = "";

                return Ok(new
                {
                    user,
                    token
                });
            }
            catch (Exception)
            {
                return NotFound(new { message = "Erro no Login" });
            }
        }



        //POST api/<LoginController>
        [HttpPost]
        [Route("paciente")]
        [AllowAnonymous]
        public async Task<ActionResult> PostAsync(Login login)
        {
            try
            {
                login.Senha = Hash.Criar(login.Senha);
                if (!_context.Pacientes.Include(x => x.Login).Any(x => x.Login.Email == login.Email.ToLower() && x.Login.Senha == login.Senha)) return NotFound(new { message = "Email ou Senha Invalido" });


                var user = await _context.Pacientes.Include(x => x.Login).Include(x => x.Nutricionista).FirstAsync(x => x.Login.Senha == login.Senha && x.Login.Email == login.Email.ToLower());
                /*

                                if (user == null)
                                    return NotFound(new { message = "Email ou Senha Invalido" });*/

                /*if (user.Login.Senha != login.Senha)
                    return NotFound(new { message = "Email ou Senha Invalido" });*/

                var token = TokenService.GerenateToken(user.Login);
                Nutricionista nutri = null;
                if (user.Fk_Nutricionista_Id > 0) { 
                    nutri = user.Nutricionista; 
                }

                user.Login.Senha = "";

                return Ok(new
                {
                    user.Id,
                    user.Nome,
                    user.Telefone,
                    user.Fator_Sensibilidade,
                    user.Tipo_Diabetes,
                    nutricionista = nutri != null ? new { nutri.Id, nutri.Nome, nutri.Endereco, nutri.Telefone, nutri.Login } : null,
                    token,
                    user.Login,
                }); ;
            }
            catch (Exception)
            {
                return NotFound(new { message = "Erro" });
            }
        }


    }
}
