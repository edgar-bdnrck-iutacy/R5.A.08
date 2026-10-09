using Microsoft.AspNetCore.Mvc;
using R5.A._08_TD3.Managers;
using R5.A._08_TD3.Models;
using R5.A._08_TD3.Models.DTO;

namespace R5.A._08_TD3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduitsController : ControllerBase
    {
        private readonly IProduitManager _manager;

        public ProduitsController(IProduitManager manager)
        {
            _manager = manager;
        }

        // GET: api/Produits
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProduitDto>>> GetProduit()
        {
            var produits = await _manager.GetAllAsync();

            return Ok(produits);
        }

        // GET: api/Produits/5
        [HttpGet("{idproduit}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProduitDetailDto>> GetProduit(int idproduit)
        {
            var produit = await _manager.GetByIdAsync(idproduit);

            if (produit == null)
            {
                return NotFound();
            }

            return Ok(produit);
        }

        // GET: api/Produits/brand/2
        [HttpGet("brand/{brandId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProduitDto>>> GetByBrand(int brandId)
        {
            var produits = await _manager.GetByBrandAsync(brandId);

            return Ok(produits);
        }

        // GET: api/Produits/2/edit
        [HttpGet("{idproduit}/edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Produit>> GetProduitForEdit(int idproduit)
        {
            var produit = await _manager.GetEntityByIdAsync(idproduit);

            if (produit == null)
            {
                return NotFound();
            }

            return Ok(new Produit
            {
                IdProduit = produit.IdProduit,
                NomProduit = produit.NomProduit,
                Description = produit.Description,
                NomPhoto = produit.NomPhoto,
                UriPhoto = produit.UriPhoto,
                IdTypeProduit = produit.IdTypeProduit,
                IdMarque = produit.IdMarque,
                StockReel = produit.StockReel,
                StockMin = produit.StockMin,
                StockMax = produit.StockMax
            });
        }

        // POST: api/Produits
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Produit>> PostProduit(Produit produit)
        {
            await _manager.AddAsync(produit);
            await _manager.SaveAsync();

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

            var existingProduit = await _manager.GetEntityByIdAsync(idproduit);

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

            await _manager.UpdateAsync(existingProduit);
            await _manager.SaveAsync();

            return NoContent();
        }

        // DELETE: api/Produits/5
        [HttpDelete("{idproduit}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduit(int idproduit)
        {
            var produit = await _manager.GetEntityByIdAsync(idproduit);

            if (produit == null)
            {
                return NotFound();
            }

            await _manager.DeleteAsync(produit);
            await _manager.SaveAsync();

            return NoContent();
        }
    }
}