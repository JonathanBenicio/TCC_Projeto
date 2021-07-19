
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;

namespace WebApi.Controllers.MySQL
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class Tipo_RefeicaosController : ControllerBase
    {
        private readonly WebApiContextMySQL _context;

        public Tipo_RefeicaosController(WebApiContextMySQL context)
        {
            _context = context;
        }

        // GET: api/Tipo_Refeicaos

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tipo_Refeicao>>> GetTipo_Refeicao()
        {
            return await _context.Tipo_Refeicaos.ToListAsync();
        }

        // GET: api/Tipo_Refeicaos/paciente/5
        //[Route("pac")]
        [HttpGet("paciente/{id}")]
        public async Task<ActionResult<List<Tipo_Refeicao>>> GetTipo_Refeicao(int id)
        {
            var tipo_Refeicao = await _context.Tipo_Refeicaos.Where(x => x.Fk_Paciente_Id == id).ToListAsync();

            if (tipo_Refeicao == null)
            {
                return NotFound();
            }

            return tipo_Refeicao;
        }



        // POST: api/Tipo_Refeicao
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Tipo_Refeicao>> PostTipo_Refeicao(Tipo_Refeicao tipo_Refeicao)
        {
            _context.Tipo_Refeicaos.Add(tipo_Refeicao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipo_Refeicao", new { id = tipo_Refeicao.Id }, tipo_Refeicao);
        }



        // DELETE: api/Tipo_Refeicao/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tipo_Refeicao>> DeleteTipo_Refeicao(int id)
        {
            var tipo_Refeicao = await _context.Tipo_Refeicaos.FindAsync(id);
            if (tipo_Refeicao == null)
            {
                return NotFound();
            }

            _context.Tipo_Refeicaos.Remove(tipo_Refeicao);
            await _context.SaveChangesAsync();

            return tipo_Refeicao;
        }

    }
}
