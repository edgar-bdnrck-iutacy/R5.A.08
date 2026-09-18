using Microsoft.AspNetCore.Mvc;
using R5.A._08_TD2.Models;
using R5.A._08_TD2.Models.Repository.Interfaces;

namespace R5.A._08_TD2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduitsController : ControllerBase
    {
        private readonly IProduitRepository _repository;

        public ProduitsController(IProduitRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Produits
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Produit>>> GetProduit()
        {
            var produits = await _repository.GetAllWithDetailsAsync();

            return Ok(produits);
        }

        // GET: api/Produits/5
        [HttpGet("{idproduit}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Produit>> GetProduit(int idproduit)
        {
            var produit = await _repository.GetByIdWithDetailsAsync(idproduit);

            if (produit == null)
            {
                return NotFound();
            }

            return Ok(produit);
        }

        // GET: api/Produits/brand/2
        [HttpGet("brand/{brandId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Produit>>> GetByBrand(int brandId)
        {
            var produits = await _repository.GetByBrandAsync(brandId);

            return Ok(produits);
        }

        // POST: api/Produits
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Produit>> PostProduit(Produit produit)
        {
            await _repository.AddAsync(produit);
            await _repository.SaveAsync();

            return CreatedAtAction(
                nameof(GetProduit),
                new { idproduit = produit.IdProduit },
                produit
            );
        }

        // PUT: api/Produits/5
        [HttpPut("{idproduit}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutProduit(int idproduit, Produit produit)
        {
            if (idproduit != produit.IdProduit)
            {
                return BadRequest();
            }

            var existingProduit = await _repository.GetByIdAsync(idproduit);

            if (existingProduit == null)
            {
                return NotFound();
            }

            existingProduit.NomProduit = produit.NomProduit;
            existingProduit.Description = produit.Description;
            existingProduit.NomPhoto = produit.NomPhoto;
            existingProduit.UriPhoto = produit.UriPhoto;
            existingProduit.IdTypeProduit = produit.IdTypeProduit;
            existingProduit.IdMarque = produit.IdMarque;
            existingProduit.StockReel = produit.StockReel;
            existingProduit.StockMin = produit.StockMin;
            existingProduit.StockMax = produit.StockMax;

            await _repository.UpdateAsync(existingProduit);
            await _repository.SaveAsync();

            return NoContent();
        }

        // DELETE: api/Produits/5
        [HttpDelete("{idproduit}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduit(int idproduit)
        {
            var produit = await _repository.GetByIdAsync(idproduit);

            if (produit == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(produit);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}