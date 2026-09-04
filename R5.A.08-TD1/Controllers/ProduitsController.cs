using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R5.A._08_TD1.Models;

[Route("api/[controller]")]
[ApiController]
public class ProduitsController : ControllerBase
{
    private readonly R508Context _context;
    public ProduitsController(R508Context context)
    {
        _context = context;
    }

    // GET: api/Produit
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produit>>> GetProduit()
    {
        return await _context.Produits.ToListAsync();
    }

    // GET: api/Produit/5
    [HttpGet("{idproduit}")]
    public async Task<ActionResult<Produit>> GetProduit(int idproduit)
    {
        var produit = await _context.Produits.FindAsync(idproduit);

        if (produit == null)
        {
            return NotFound();
        }

        return produit;
    }

    // PUT: api/Produit/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{idproduit}")]
    public async Task<IActionResult> PutProduit(int idproduit, Produit produit)
    {
        if (idproduit != produit.IdProduit)
        {
            return BadRequest();
        }

        _context.Entry(produit).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProduitExists(idproduit))
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

    // POST: api/Produit
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Produit>> PostProduit(Produit produit)
    {
        _context.Produits.Add(produit);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProduit", new { idproduit = produit.IdProduit }, produit);
    }

    // DELETE: api/Produit/5
    [HttpDelete("{idproduit}")]
    public async Task<IActionResult> DeleteProduit(int idproduit)
    {
        var produit = await _context.Produits.FindAsync(idproduit);
        if (produit == null)
        {
            return NotFound();
        }

        _context.Produits.Remove(produit);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProduitExists(int idproduit)
    {
        return _context.Produits.Any(e => e.IdProduit == idproduit);
    }
}
