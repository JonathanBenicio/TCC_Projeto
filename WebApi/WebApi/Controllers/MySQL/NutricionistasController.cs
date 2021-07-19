using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Services;

namespace WebApi.Controllers.MySQL
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class NutricionistasController : ControllerBase
    {
        private readonly WebApiContextMySQL _context;

        public NutricionistasController(WebApiContextMySQL context)
        {
            _context = context;
        }

        // GET: api/Nutricionistas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nutricionista>>> GetNutricionista()
        {
            return await _context.Nutricionistas.ToListAsync();
        }

        // GET: api/Nutricionistas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Nutricionista>> GetNutricionista(int id)
        {
            var nutricionista = await _context.Nutricionistas.FindAsync(id);

            if (nutricionista == null)
            {
                return NotFound();
            }

            return nutricionista;
        }

        // PUT: api/Nutricionistas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNutricionista(int id, Nutricionista nutricionista)
        {
            if (id != nutricionista.Id)
            {
                return BadRequest();
            }

            _context.Entry(nutricionista).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NutricionistaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Nutricionistas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Nutricionista>> PostNutricionista(Nutricionista Nutri)
        {
            /* var ultimoIdLogin = _context.Login.Last();*/
            try
            {

                Nutri.Login.Senha = Hash.Criar(Nutri.Login.Senha);
                Nutri.Login.Email = Nutri.Login.Email.ToLower();

                if (_context.Logins.Any(x => x.Email == Nutri.Login.Email)) return NotFound(new { message = "Email já cadastrado" });

                _context.Logins.Add(Nutri.Login);
                await _context.SaveChangesAsync();
                Nutri.Fk_Login_Id = Nutri.Login.Id;
                _context.Nutricionistas.Add(Nutri);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetNutricionista", new { id = Nutri.Id }, Nutri);
            }
            catch (System.Exception)
            {
                return NotFound(new { message = "Erro" });
                throw;
            }

        }

        // DELETE: api/Nutricionistas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Nutricionista>> DeleteNutricionista(int id)
        {
            var nutricionista = await _context.Nutricionistas.FindAsync(id);
            if (nutricionista == null)
            {
                return NotFound();
            }

            _context.Nutricionistas.Remove(nutricionista);
            await _context.SaveChangesAsync();

            return nutricionista;
        }

        private bool NutricionistaExists(int id)
        {
            return _context.Nutricionistas.Any(e => e.Id == id);
        }
    }
}
