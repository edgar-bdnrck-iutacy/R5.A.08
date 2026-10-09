using Microsoft.AspNetCore.Mvc;
using R5.A._08_TD3.Managers;
using R5.A._08_TD3.Models;
using R5.A._08_TD3.Models.DTO;

namespace R5.A._08_TD3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeProduitsController : ControllerBase
    {
        private readonly ITypeProduitManager _manager;

        public TypeProduitsController(ITypeProduitManager manager)
        {
            _manager = manager;
        }

        // GET: api/TypeProduits
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TypeProduitDto>>> GetTypeProduits()
        {
            var types = await _manager.GetAllAsync();

            return Ok(types);
        }

        // GET: api/TypeProduits/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TypeProduitDto>> GetTypeProduit(int id)
        {
            var typeProduit = await _manager.GetByIdAsync(id);

            if (typeProduit == null)
            {
                return NotFound();
            }

            return Ok(typeProduit);
        }

        // POST: api/TypeProduits
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TypeProduit>> PostTypeProduit(TypeProduit typeProduit)
        {
            var created = await _manager.AddAsync(typeProduit);

            return CreatedAtAction(
                nameof(GetTypeProduit),
                new { id = created.IdTypeProduit },
                created
            );
        }

        // PUT: api/TypeProduits/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutTypeProduit(int id, TypeProduit typeProduit)
        {
            if (id != typeProduit.IdTypeProduit)
            {
                return BadRequest();
            }

            if (!_manager.Exists(id))
            {
                return NotFound();
            }

            await _manager.UpdateAsync(typeProduit);

            return NoContent();
        }

        // DELETE: api/TypeProduits/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTypeProduit(int id)
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