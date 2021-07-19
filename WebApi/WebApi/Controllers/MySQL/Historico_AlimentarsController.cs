using AnalysisInterface.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using System.Linq.Dynamic.Core;
using System.Globalization;

namespace WebApi.Controllers.MySQL
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class Historico_AlimentarsController : ControllerBase
    {
        private readonly WebApiContextMySQL _context;

        public Historico_AlimentarsController(WebApiContextMySQL context)
        {
            _context = context;
        }


        // GET: api/Historico_Alimentars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Historico_Alimentar>>> GetAlimento_Historico()
        {
            return await _context.Historico_Alimentars.Include(x =>x.Alimento_Historicos).ToListAsync();
        }

        // GET: api/Historico_Alimentars/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Historico_Alimentar>> GetHistorico_Alimentar(int id)
        {
            try
            {
                var init = _context.Historico_Alimentars.Where(x => x.Fk_Paciente_Id == id).Include(x => x.Alimento_Historicos).Include(x => x.Tipo_Refeicao);

                var total = _context.Historico_Alimentars.Where(x => x.Fk_Paciente_Id == id).Count();
                var tempResult = init.Select(x => new
                {
                    id = x.Id,
                    refeicao = x.Tipo_Refeicao.Nome,
                    glicemia_Obtida = x.Glicemia_Obtida,
                    glicemia_Alvo = x.Glicemia_Alvo,
                    carboidratos_Total = x.Carboidratos_Total,
                    insulina_Calculada = x.Insulina_Calculada,
                    data_Hora = x.Data_Hora.Date.ToString("dd/MM/yyyy"),
                    data = x.Data_Hora.Date
                });

                tempResult = tempResult.OrderByDescending(x => x.id);

                var result = await tempResult
                      .ToArrayAsync();

                return new JsonResult(new
                {

                    recordsTotal = total,
                    recordsFiltered = result.Count() != total ? tempResult.Count() : total,
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



        // POST: api/Historico_Alimentars
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("{id}")]
        [Authorize]
        public async Task<ActionResult> PostHistorico_Alimentar([FromForm] DataTableAjaxPostModel model, [FromRoute] int id)
        {
            try
            {

                var init = _context.Historico_Alimentars.Where(x => x.Fk_Paciente_Id == id).Include(x => x.Alimento_Historicos).Include(x => x.Tipo_Refeicao);

                var total = _context.Historico_Alimentars.Where(x => x.Fk_Paciente_Id == id).Count();
                IQueryable<Historico_Alimentar> tempResult = init;



                var temp = tempResult.Select(x => new
                {
                    id = x.Id,
                    refeicao = x.Tipo_Refeicao.Nome,
                    glicemia_Obtida = x.Glicemia_Obtida,
                    glicemia_Alvo = x.Glicemia_Alvo,
                    carboidratos_Total = x.Carboidratos_Total,
                    insulina_Calculada = x.Insulina_Calculada,
                    data_Hora = x.Data_Hora.Date.ToString("G", DateTimeFormatInfo.InvariantInfo),
                    data = x.Data_Hora.Date
                });

                if (model.order != null)
                {
                    
                        var sortBy = model.columns[model.order[0].column].data;
                        var sortDir = model.order[0].dir.ToLower();
                        temp = temp.OrderBy(sortBy + " " + sortDir);
                   
                }
                else
                {
                    temp = temp.OrderByDescending(x => x.id);
                }

                var result = await temp
                      .Skip(model.start)
                      .Take(model.length)
                      .ToArrayAsync();

                return new JsonResult(new
                {
                    model.draw,
                    recordsTotal = total,
                    recordsFiltered = result.Count() != total ? temp.Count() : total,
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

        [HttpPost]
        public async Task<ActionResult<Historico_Alimentar>> PostTipo_Refeicao(Historico_Alimentar historico_Alimentar)
        {
            _context.Historico_Alimentars.Add(historico_Alimentar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHistorico_Alimentar", new { id = historico_Alimentar.Id }, historico_Alimentar);
        }



    }
}
