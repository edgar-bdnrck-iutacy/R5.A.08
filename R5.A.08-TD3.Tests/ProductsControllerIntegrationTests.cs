using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R5.A._08_TD3.Controllers;
using R5.A._08_TD3.Managers;
using R5.A._08_TD3.Models;
using R5.A._08_TD3.Models.DTO;
using R5.A._08_TD3.Models.Repository.Implementations;
using R5.A._08_TD3.Profiles;

namespace R5.A._08_TD3.Tests
{
    [TestClass]
    public class ProductsControllerIntegrationTests
    {
        private R508Context _context = null!;
        private ProductRepository _repository = null!;
        private ProduitsController _controller = null!;
        private IMapper _mapper = null!;

        [TestInitialize]
        public async Task Setup()
        {
            var options = new DbContextOptionsBuilder<R508Context>()
                .UseNpgsql(
                    "Host=localhost;Port=5432;Database=ProductsTestDB;Username=postgres;Password=postgres"
                )
                .Options;

            _context = new R508Context(options);

            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();

            _repository = new ProductRepository(_context);

            var mapperConfig = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile<MappingProfile>();
                },
                Microsoft.Extensions.Logging.Abstractions.NullLoggerFactory.Instance
            );

            _mapper = mapperConfig.CreateMapper();

            var manager = new ProduitManager(
                _repository,
                _mapper
            );

            _controller = new ProduitsController(manager);
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            await _context.Database.EnsureDeletedAsync();
            await _context.DisposeAsync();
        }

        [TestMethod]
        public async Task PostProduit_ThenGetProduit_ReturnsPersistedProduct()
        {
            var marque = new Marque
            {
                NomMarque = "Logitech"
            };

            var typeProduit = new TypeProduit
            {
                NomTypeProduit = "Mouse"
            };

            _context.Marques.Add(marque);
            _context.TypesProduit.Add(typeProduit);
            await _context.SaveChangesAsync();

            var produit = new Produit
            {
                NomProduit = "G502 HERO",
                Description = "Gaming mouse",
                IdMarque = marque.IdMarque,
                IdTypeProduit = typeProduit.IdTypeProduit,
                StockReel = 10,
                StockMin = 2,
                StockMax = 20
            };

            var postResult = await _controller.PostProduit(produit);

            var createdResult =
                postResult.Result as CreatedAtActionResult;

            Assert.IsNotNull(createdResult);
            Assert.AreEqual(201, createdResult.StatusCode);
            Assert.IsTrue(produit.IdProduit > 0);

            var getResult =
                await _controller.GetProduit(produit.IdProduit);

            var okResult =
                getResult.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var returnedProduit =
                okResult.Value as ProduitDetailDto;

            Assert.IsNotNull(returnedProduit);
            Assert.AreEqual(
                "G502 HERO",
                returnedProduit.Nom
            );
            Assert.AreEqual(
                "Logitech",
                returnedProduit.Marque
            );
            Assert.AreEqual(
                "Mouse",
                returnedProduit.Type
            );
        }

        [TestMethod]
        public async Task PutProduit_ExistingProduct_UpdatesPersistedProduct()
        {
            var marque = new Marque
            {
                NomMarque = "Logitech"
            };

            var typeProduit = new TypeProduit
            {
                NomTypeProduit = "Mouse"
            };

            _context.Marques.Add(marque);
            _context.TypesProduit.Add(typeProduit);
            await _context.SaveChangesAsync();

            var produit = new Produit
            {
                NomProduit = "G502 HERO",
                Description = "Gaming mouse",
                IdMarque = marque.IdMarque,
                IdTypeProduit = typeProduit.IdTypeProduit,
                StockReel = 10,
                StockMin = 2,
                StockMax = 20
            };

            _context.Produits.Add(produit);
            await _context.SaveChangesAsync();

            var updatedProduit = new Produit
            {
                IdProduit = produit.IdProduit,
                NomProduit = "G502 HERO Updated",
                Description = "Gaming mouse",
                IdMarque = marque.IdMarque,
                IdTypeProduit = typeProduit.IdTypeProduit,
                StockReel = 15,
                StockMin = 2,
                StockMax = 20
            };

            var putResult = await _controller.PutProduit(
                produit.IdProduit,
                updatedProduit
            );

            Assert.IsInstanceOfType(
                putResult,
                typeof(NoContentResult)
            );

            var getResult =
                await _controller.GetProduit(produit.IdProduit);

            var okResult =
                getResult.Result as OkObjectResult;

            Assert.IsNotNull(okResult);

            var returnedProduit =
                okResult.Value as ProduitDetailDto;

            Assert.IsNotNull(returnedProduit);
            Assert.AreEqual(
                "G502 HERO Updated",
                returnedProduit.Nom
            );
            Assert.AreEqual(
                15,
                returnedProduit.Stock
            );
        }

        [TestMethod]
        public async Task DeleteProduit_ExistingProduct_ThenGetReturnsNotFound()
        {
            var marque = new Marque
            {
                NomMarque = "Logitech"
            };

            var typeProduit = new TypeProduit
            {
                NomTypeProduit = "Mouse"
            };

            _context.Marques.Add(marque);
            _context.TypesProduit.Add(typeProduit);
            await _context.SaveChangesAsync();

            var produit = new Produit
            {
                NomProduit = "G502 HERO",
                Description = "Gaming mouse",
                IdMarque = marque.IdMarque,
                IdTypeProduit = typeProduit.IdTypeProduit,
                StockReel = 10,
                StockMin = 2,
                StockMax = 20
            };

            _context.Produits.Add(produit);
            await _context.SaveChangesAsync();

            var deleteResult =
                await _controller.DeleteProduit(produit.IdProduit);

            Assert.IsInstanceOfType(
                deleteResult,
                typeof(NoContentResult)
            );

            var getResult =
                await _controller.GetProduit(produit.IdProduit);

            Assert.IsInstanceOfType(
                getResult.Result,
                typeof(NotFoundResult)
            );
        }

        [TestMethod]
        public async Task PostProduit_InvalidBrandId_ThrowsDbUpdateException()
        {
            var typeProduit = new TypeProduit
            {
                NomTypeProduit = "Mouse"
            };

            _context.TypesProduit.Add(typeProduit);
            await _context.SaveChangesAsync();

            var produit = new Produit
            {
                NomProduit = "Invalid Product",
                Description = "Product with invalid brand",
                IdMarque = 999,
                IdTypeProduit = typeProduit.IdTypeProduit,
                StockReel = 10,
                StockMin = 2,
                StockMax = 20
            };

            await Assert.ThrowsExceptionAsync<DbUpdateException>(
                async () =>
                    await _controller.PostProduit(produit)
            );
        }
    }
}