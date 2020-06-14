using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        public readonly ProAgilContext _dbcontext;
        public ValuesController(ProAgilContext context){
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
                return Ok(await _dbcontext.Eventos.FirstOrDefaultAsync(x => x.Id == id));
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados não encontrado");
            }
        }
    }
}