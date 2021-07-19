using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;

namespace WebApi.Controllers.MySQL
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class AlimentosController : ControllerBase
    {
        private readonly WebApiContextMySQL _context;

        private readonly IConfiguration _configuration;

        public AlimentosController(WebApiContextMySQL context, IConfiguration Configuration)
        {
            _configuration = Configuration;
            _context = context;
        }

        // GET: api/Alimentos
        [HttpGet]
        public async Task<ActionResult<dynamic>> GetAlimento()
        {
            return await _context.Alimentos.Select(x => new
            {
                x.Id,
                x.Nome,
                x.Marca,
                x.Porcao_Tipo,
                x.Porcao_Quantidade,
                x.Porcao_Carboidratos,
                x.Tipo,
                Foto = System.IO.File.ReadAllBytes(_configuration["Anexos:Caminho"] + x.Foto)
            }).ToListAsync();

        }

        // POST: api/Alimentos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Alimento>> PostAlimento(Alimento alimento)
        {
            _context.Alimentos.Add(alimento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlimento", new { id = alimento.Id }, alimento);
        }
    }
}
