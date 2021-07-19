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

namespace WebApi.Controllers.MySQL
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class Alimento_HistoricosController : ControllerBase
    {
        private readonly WebApiContextMySQL _context;

        public Alimento_HistoricosController(WebApiContextMySQL context)
        {
            _context = context;
        }

        // GET: api/Alimento_Historico
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alimento_Historico>>> GetAlimento_Historico()
        {
            return await _context.Alimento_Historicos.ToListAsync();
        }

        // GET: api/Alimento_Historico/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Alimento_Historico>> GetAlimento_Historico(int id)
        {
            try
            {

                var init = _context.Alimento_Historicos.Where(x => x.Fk_Historico_Alimentar_Id == id).Include(x => x.Historico_Alimentar).Include(x => x.Alimento);

                var total = _context.Alimento_Historicos.Where(x => x.Fk_Historico_Alimentar_Id == id).Count();
                var tempResult = init.Select(x => new
                {
                    id = x.Id,
                    quantidade = (x.Quantidade.ToString() + x.Alimento.Porcao_Tipo),
                    carboTotal = x.Carboidratos_Total,
                    alimento = x.Alimento.Nome,
                    marca = x.Alimento.Marca,
                    tipo = x.Alimento.Tipo,
                    porcao = (x.Alimento.Porcao_Quantidade.ToString() + x.Alimento.Porcao_Tipo),
                    carboPorcao = x.Alimento.Porcao_Carboidratos,
                });


                var result = await tempResult.ToArrayAsync();

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

        // POST: api/Alimento_Historico/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("{id}")]
        [Authorize]
        public async Task<ActionResult<Alimento_Historico>> PostAlimento_Historico([FromForm] DataTableAjaxPostModel model, [FromForm] String pesquisa,[FromForm] int selectColuna, [FromRoute] int id)
        {
            try
            {

                var init = _context.Alimento_Historicos.Where(x => x.Fk_Historico_Alimentar_Id == id).Include(x => x.Historico_Alimentar).Include(x => x.Alimento);

                var total = _context.Alimento_Historicos.Where(x => x.Fk_Historico_Alimentar_Id == id).Count();
                IQueryable<Alimento_Historico> tempResult = init;

                if (!string.IsNullOrEmpty(pesquisa))
                {
                    if (selectColuna == 1)
                    {

                        tempResult = init.Where(x => x.Alimento.Nome.Contains(pesquisa));
                    }
                    else if (selectColuna == 2)
                    {

                        tempResult = init.Where(x => x.Alimento.Marca.Contains(pesquisa));
                    }
                    else if (selectColuna == 3)
                    {

                        tempResult = init.Where(x => x.Alimento.Tipo.Contains(pesquisa));
                    }

                }

                var temp = tempResult.Select(x => new
                {
                    id = x.Id,
                    quantidade = (x.Quantidade.ToString() + x.Alimento.Porcao_Tipo),
                    carboTotal = x.Carboidratos_Total,
                    alimento = x.Alimento.Nome,
                    marca = x.Alimento.Marca,
                    tipo = x.Alimento.Tipo,
                    porcao = (x.Alimento.Porcao_Quantidade.ToString() + x.Alimento.Porcao_Tipo),
                    carboPorcao = x.Alimento.Porcao_Carboidratos,
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
                    draw = model.draw,
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


        [HttpGet("grafico/{id}")]
        [Authorize]
        public ActionResult<dynamic> GetGrafico(int id)
        {
            var alimento_Historico = _context.Alimento_Historicos.Where(x => x.Fk_Historico_Alimentar_Id == id);

            var result = alimento_Historico.GroupBy(x => x.Fk_Historico_Alimentar_Id).Select(x => new
            {
                quanti = x.Count()

            }).ToArray();


            return Ok(alimento_Historico);
        }

        /*// POST: api/Alimento_Historico/
        [HttpPost]
        public async Task<ActionResult<Alimento_Historico>> PostTipo_Refeicao(Alimento_Historico tipo_Refeicao)
        {
            _context.Tipo_Refeicaos.Add(tipo_Refeicao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipo_Refeicao", new { id = tipo_Refeicao.Id }, tipo_Refeicao);
        }*/

    }
}
