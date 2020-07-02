using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IRepository _repo;
        public EventoController(IRepository repo)
        {
            _repo = repo;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repo.GetAllEventoAsync(true);
                return Ok(results);
            }
            catch( System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados não encontrado");
            }
        }
        [HttpGet("id")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var results = await _repo.GetEventosAsyncById(id, true);
                return Ok(results);
            }
            catch( System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados não encontrado");
            }
        }
         [HttpGet("getByTema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                var results = await _repo.GetAllEventoAsyncByTema(tema, true);
                return Ok(results);
            }
            catch( System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados não encontrado");
            }
        }
         [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                _repo.Add(model);
                if(await _repo.SaveChangesAsync())
                {
                    return Created($"/api/evento/{model.Id}", model);
                }
            }
            catch( System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados não encontrado");
            }
            return BadRequest();
        }
         [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento model)
        {
            try
            {
                var evento = await _repo.GetEventosAsyncById(id, false);
                if(evento == null) return NotFound();
                _repo.Update(model);
                if(await _repo.SaveChangesAsync())
                {
                    return Created($"/api/evento/{model.Id}", model);
                }
            }
            catch( System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados não encontrado");
            }
            return BadRequest();
        }
         [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var evento = await _repo.GetEventosAsyncById(id, false);
                if(evento == null) return NotFound();

                _repo.Delete(evento);

                if(await _repo.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch( System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados não encontrado");
            }
            return BadRequest();
        }
    }
}