using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using R5.A._08_TD3.Controllers;
using R5.A._08_TD3.Managers;
using R5.A._08_TD3.Models;
using R5.A._08_TD3.Models.DTO;
using R5.A._08_TD3.Models.Repository.Interfaces;
using R5.A._08_TD3.Profiles;

namespace R5.A._08_TD3.Tests
{
    [TestClass]
    public class ProduitsControllerTests
    {
        private readonly IMapper _mapper;

        public ProduitsControllerTests()
        {
            var mapperConfig = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile<MappingProfile>();
                },
                Microsoft.Extensions.Logging.Abstractions.NullLoggerFactory.Instance
            );

            _mapper = mapperConfig.CreateMapper();
        }

        [TestMethod]
        public async Task GetProduit_ExistingId_ReturnsOkResult()
        {
            var produit = new Produit
            {
                IdProduit = 1,
                NomProduit = "G502 HERO",
                IdMarque = 1,
                IdTypeProduit = 1,
                Marque = new Marque
                {
                    IdMarque = 1,
                    NomMarque = "Logitech"
                },
                TypeProduit = new TypeProduit
                {
                    IdTypeProduit = 1,
                    NomTypeProduit = "Mouse"
                }
            };

            var mockRepository = new Mock<IProduitRepository>();

            mockRepository
                .Setup(r => r.GetByIdWithDetailsAsync(1))
                .ReturnsAsync(produit);

            var manager = new ProduitManager(
                mockRepository.Object,
                _mapper
            );

            var controller = new ProduitsController(manager);

            var result = await controller.GetProduit(1);

            var okResult = result.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var returnedProduit = okResult.Value as ProduitDetailDto;

            Assert.IsNotNull(returnedProduit);
            Assert.AreEqual(produit.IdProduit, returnedProduit.Id);
            Assert.AreEqual(produit.NomProduit, returnedProduit.Nom);
            Assert.AreEqual("Logitech", returnedProduit.Marque);
            Assert.AreEqual("Mouse", returnedProduit.Type);
        }

        [TestMethod]
        public async Task GetProduit_UnknownId_ReturnsNotFoundResult()
        {
            var mockRepository = new Mock<IProduitRepository>();

            mockRepository
                .Setup(r => r.GetByIdWithDetailsAsync(999))
                .ReturnsAsync((Produit?)null);

            var manager = new ProduitManager(
                mockRepository.Object,
                _mapper
            );

            var controller = new ProduitsController(manager);

            var result = await controller.GetProduit(999);

            Assert.IsInstanceOfType(
                result.Result,
                typeof(NotFoundResult)
            );
        }

        [TestMethod]
        public async Task PostProduit_ValidProduct_ReturnsCreatedAtAction()
        {
            var produit = new Produit
            {
                IdProduit = 1,
                NomProduit = "G502 HERO",
                IdMarque = 1,
                IdTypeProduit = 1
            };

            var mockRepository = new Mock<IProduitRepository>();

            mockRepository
                .Setup(r => r.AddAsync(produit))
                .Returns(Task.CompletedTask);

            mockRepository
                .Setup(r => r.SaveAsync())
                .Returns(Task.CompletedTask);

            var manager = new ProduitManager(
                mockRepository.Object,
                _mapper
            );

            var controller = new ProduitsController(manager);

            var result = await controller.PostProduit(produit);

            var createdResult = result.Result as CreatedAtActionResult;

            Assert.IsNotNull(createdResult);
            Assert.AreEqual(201, createdResult.StatusCode);
            Assert.AreEqual(
                nameof(ProduitsController.GetProduit),
                createdResult.ActionName
            );
            Assert.AreEqual(produit, createdResult.Value);

            mockRepository.Verify(
                r => r.AddAsync(produit),
                Times.Once
            );

            mockRepository.Verify(
                r => r.SaveAsync(),
                Times.Once
            );
        }

        [TestMethod]
        public async Task PutProduit_IdMismatch_ReturnsBadRequest()
        {
            var produit = new Produit
            {
                IdProduit = 2,
                NomProduit = "G502 HERO",
                IdMarque = 1,
                IdTypeProduit = 1
            };

            var mockRepository = new Mock<IProduitRepository>();

            var manager = new ProduitManager(
                mockRepository.Object,
                _mapper
            );

            var controller = new ProduitsController(manager);

            var result = await controller.PutProduit(1, produit);

            Assert.IsInstanceOfType(
                result,
                typeof(BadRequestResult)
            );

            mockRepository.Verify(
                r => r.UpdateAsync(It.IsAny<Produit>()),
                Times.Never
            );

            mockRepository.Verify(
                r => r.SaveAsync(),
                Times.Never
            );
        }

        [TestMethod]
        public async Task DeleteProduit_ExistingId_ReturnsNoContent()
        {
            var produit = new Produit
            {
                IdProduit = 1,
                NomProduit = "G502 HERO",
                IdMarque = 1,
                IdTypeProduit = 1
            };

            var mockRepository = new Mock<IProduitRepository>();

            mockRepository
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(produit);

            mockRepository
                .Setup(r => r.DeleteAsync(produit))
                .Returns(Task.CompletedTask);

            mockRepository
                .Setup(r => r.SaveAsync())
                .Returns(Task.CompletedTask);

            var manager = new ProduitManager(
                mockRepository.Object,
                _mapper
            );

            var controller = new ProduitsController(manager);

            var result = await controller.DeleteProduit(1);

            Assert.IsInstanceOfType(
                result,
                typeof(NoContentResult)
            );

            mockRepository.Verify(
                r => r.DeleteAsync(produit),
                Times.Once
            );

            mockRepository.Verify(
                r => r.SaveAsync(),
                Times.Once
            );
        }
    }
}