using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Model;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        public readonly DataContext _dbcontext;
        public ValuesController(DataContext context){
            _dbcontext = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAction()
        {
                try
                {
                    return Ok(await _dbcontext.Eventos.ToListAsync());
                }
                catch (System.Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados não encontrado");
                }
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id){
            try
            {
                return Ok(await _dbcontext.Eventos.FirstOrDefaultAsync(x => x.EventoId == id));
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados não encontrado");
            }
        }
    }
}