using AnalysisInterface.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using System.Linq.Dynamic.Core;
using WebApi.Services;

namespace WebApi.Controllers.MySQL
{
    [Route("v1/api/pacientes")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly WebApiContextMySQL _context;

        public PacientesController(WebApiContextMySQL context)
        {
            _context = context;
        }

        // GET: api/Pacientes
        [HttpPost("search/{id}")]
        [Authorize]
        public async Task<ActionResult> PostPaciente([FromForm] DataTableAjaxPostModel model, [FromForm] String pesquisa, [FromForm] int selectColuna, [FromForm] int selectDiabetes, [FromRoute] int id)
        {
            try
            {
                var pac = _context.Pacientes.Where(x => x.Fk_Nutricionista_Id == id);


                IQueryable<Paciente> tempResult = pac;

                var total = pac.Count();


                if (!string.IsNullOrEmpty(pesquisa))
                {
                    if (selectColuna == 1)
                    {
                        tempResult = pac.Where(x => x.Nome.Contains(pesquisa));
                    }
                    else if (selectColuna == 2)
                    {
                        tempResult = pac.Where(x => x.Telefone.Contains(pesquisa));
                    }

                }
                if (selectDiabetes > 0)
                {
                    Diabetes dia = Diabetes.Tipo_1;
                    if (selectDiabetes == 1) dia = Diabetes.Tipo_1;
                    if (selectDiabetes == 2) dia = Diabetes.Tipo_2;
                    if (selectDiabetes == 3) dia = Diabetes.Gravidez;
                    if (selectDiabetes == 4) dia = Diabetes.Pré_Diabetes;

                    tempResult = pac.Where(x => x.Tipo_Diabetes == dia);
                }


                var temp = tempResult.Select(x => new
                {
                    id = x.Id,
                    nome = x.Nome,
                    fator_Sen = x.Fator_Sensibilidade,
                    tipo_Diab = x.Tipo_Diabetes,
                    telefone = x.Telefone
                });

                if (model.order != null)
                {
                    var sortBy = model.columns[model.order[0].column].data;
                    var sortDir = model.order[0].dir.ToLower();
                    temp = temp.OrderBy(sortBy+" " + sortDir);
                }
                else
                {
                    temp = temp.OrderByDescending(x => x.id);
                }

                var result = await temp
                      .Skip(model.start)
                      .Take(model.length).ToListAsync();

                return new JsonResult(new
                {
                    draw = model.draw,
                    recordsTotal = total,
                    recordsFiltered = result.Count() != total ? tempResult.Count() : total,
                    data = result

                });
            }
            catch (Exception e)
            {
                        return new JsonResult(new
                {
                    model.draw,
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    data = ""
                });
            }
        }

        // GET: api/Pacientes/search/5
        [HttpGet("search/{id}")]
        [Authorize]
        public async Task<ActionResult<Paciente>> GetPaciente(int id)
        {
            try
            {
                var pac = _context.Pacientes.Where(x => x.Fk_Nutricionista_Id == id);


                var tempResult = pac.Select(x => new
                {
                    id = x.Id,
                    nome = x.Nome,
                    fator_Sen = x.Fator_Sensibilidade,
                    tipo_Diab = x.Tipo_Diabetes.ToString().Replace("_", " "),
                    telefone = x.Telefone
                });



                var total = pac.Count();
                var result = await tempResult
                      .ToArrayAsync();

                return new JsonResult(new
                {
                    recordsTotal = total,
                    recordsFiltered = total,
                    data = result

                });
            }
            catch (Exception e)
            {
                return new JsonResult(new
                {
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    data = ""
                });
            }
        }

        // PUT: api/Pacientes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaciente(int id, Paciente paciente)
        {
            if (id != paciente.Id)
            {
                return BadRequest();
            }

            _context.Entry(paciente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacienteExists(id))
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

        // POST: api/Pacientes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Paciente>> PostPaciente(Paciente paciente)
        {
            paciente.Login.Senha = Hash.Criar(paciente.Login.Senha);
            paciente.Nutricionista = _context.Nutricionistas.First(x => x.Login.Email == paciente.Nutricionista.Login.Email);
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaciente", new { id = paciente.Id }, paciente);
        }

        // DELETE: api/Pacientes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Paciente>> DeletePaciente(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }

            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();

            return paciente;
        }

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.Id == id);
        }
    }
}
