using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R5.A._08_TD1.Models;

[Route("api/[controller]")]
[ApiController]
public class MarquesController : ControllerBase
{
    private readonly R508Context _context;
    public MarquesController(R508Context context)
    {
        _context = context;
    }

    // GET: api/Marque
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Marque>>> GetMarque()
    {
        return await _context.Marques.ToListAsync();
    }

    // GET: api/Marque/5
    [HttpGet("{idmarque}")]
    public async Task<ActionResult<Marque>> GetMarque(int idmarque)
    {
        var marque = await _context.Marques.FindAsync(idmarque);

        if (marque == null)
        {
            return NotFound();
        }

        return marque;
    }

    // PUT: api/Marque/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{idmarque}")]
    public async Task<IActionResult> PutMarque(int? idmarque, Marque marque)
    {
        if (idmarque != marque.IdMarque)
        {
            return BadRequest();
        }

        _context.Entry(marque).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MarqueExists(idmarque))
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

    // POST: api/Marque
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Marque>> PostMarque(Marque marque)
    {
        _context.Marques.Add(marque);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetMarque", new { idmarque = marque.IdMarque }, marque);
    }

    // DELETE: api/Marque/5
    [HttpDelete("{idmarque}")]
    public async Task<IActionResult> DeleteMarque(int? idmarque)
    {
        var marque = await _context.Marques.FindAsync(idmarque);
        if (marque == null)
        {
            return NotFound();
        }

        _context.Marques.Remove(marque);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool MarqueExists(int? idmarque)
    {
        return _context.Marques.Any(e => e.IdMarque == idmarque);
    }
}
