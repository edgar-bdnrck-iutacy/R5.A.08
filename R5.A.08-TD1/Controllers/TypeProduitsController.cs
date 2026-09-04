using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R5.A._08_TD1.Models;

[Route("api/[controller]")]
[ApiController]
public class TypeProduitsController : ControllerBase
{
    private readonly R508Context _context;
    public TypeProduitsController(R508Context context)
    {
        _context = context;
    }

    // GET: api/TypeProduit
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TypeProduit>>> GetTypeProduit()
    {
        return await _context.TypesProduit.ToListAsync();
    }

    // GET: api/TypeProduit/5
    [HttpGet("{idtypeproduit}")]
    public async Task<ActionResult<TypeProduit>> GetTypeProduit(int idtypeproduit)
    {
        var typeproduit = await _context.TypesProduit.FindAsync(idtypeproduit);

        if (typeproduit == null)
        {
            return NotFound();
        }

        return typeproduit;
    }

    // PUT: api/TypeProduit/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{idtypeproduit}")]
    public async Task<IActionResult> PutTypeProduit(int? idtypeproduit, TypeProduit typeproduit)
    {
        if (idtypeproduit != typeproduit.IdTypeProduit)
        {
            return BadRequest();
        }

        _context.Entry(typeproduit).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TypeProduitExists(idtypeproduit))
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

    // POST: api/TypeProduit
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<TypeProduit>> PostTypeProduit(TypeProduit typeproduit)
    {
        _context.TypesProduit.Add(typeproduit);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTypeProduit", new { idtypeproduit = typeproduit.IdTypeProduit }, typeproduit);
    }

    // DELETE: api/TypeProduit/5
    [HttpDelete("{idtypeproduit}")]
    public async Task<IActionResult> DeleteTypeProduit(int? idtypeproduit)
    {
        var typeproduit = await _context.TypesProduit.FindAsync(idtypeproduit);
        if (typeproduit == null)
        {
            return NotFound();
        }

        _context.TypesProduit.Remove(typeproduit);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TypeProduitExists(int? idtypeproduit)
    {
        return _context.TypesProduit.Any(e => e.IdTypeProduit == idtypeproduit);
    }
}
