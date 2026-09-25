using Microsoft.AspNetCore.Mvc;
using R5.A._08_TD3.Managers;
using R5.A._08_TD3.Models;
using R5.A._08_TD3.Models.DTO;

namespace R5.A._08_TD3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarquesController : ControllerBase
    {
        private readonly IMarqueManager _manager;

        public MarquesController(IMarqueManager manager)
        {
            _manager = manager;
        }

        // GET: api/Marques
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarqueDto>>> GetMarques()
        {
            var marques = await _manager.GetAllAsync();

            return Ok(marques);
        }

        // GET: api/Marques/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MarqueDto>> GetMarque(int id)
        {
            var marque = await _manager.GetByIdAsync(id);

            if (marque == null)
            {
                return NotFound();
            }

            return Ok(marque);
        }

        // PUT: api/Marques/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMarque(
            int id,
            Marque marque)
        {
            if (id != marque.IdMarque)
            {
                return BadRequest();
            }

            if (!_manager.Exists(id))
            {
                return NotFound();
            }

            await _manager.UpdateAsync(marque);

            return NoContent();
        }

        // POST: api/Marques
        [HttpPost]
        public async Task<ActionResult<Marque>> PostMarque(
            Marque marque)
        {
            var createdMarque =
                await _manager.AddAsync(marque);

            return CreatedAtAction(
                nameof(GetMarque),
                new { id = createdMarque.IdMarque },
                createdMarque
            );
        }

        // DELETE: api/Marques/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarque(int id)
        {
            if (!_manager.Exists(id))
            {
                return NotFound();
            }

            await _manager.DeleteAsync(id);

            return NoContent();
        }
    }
}