using Agency.Controllers;
using Agency.Interfaces;
using Agency.Models;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;

namespace Agency.Tests.Controllers
{
    [TestClass]
    public class NekretnineControllerTest
    {
        [TestMethod]
        public void GetReturnsByIdOk()
        {
            // Arrange
            var mockRepository = new Mock<INekretnineRepository>();
            mockRepository.Setup(x => x.GetById(1)).Returns(new Nekretnina { Id = 1, Mesto = "Novi Sad", AgencijskaOznaka = "Nek01", GodinaIzgradnje = 1974, Kvadratura = 50, Cena = 40000, AgentId = 1 });

            var controller = new NekretnineController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.GetById(1);
            var contentResult = actionResult as OkNegotiatedContentResult<Nekretnina>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.Id);
        }

        [TestMethod]
        public void DeleteReturnsNotFound()
        {
            // Arrange 
            var mockRepository = new Mock<INekretnineRepository>();
            var controller = new NekretnineController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PutReturnsBadRequest()
        {
            // Arrange
            var mockRepository = new Mock<INekretnineRepository>();
            var controller = new NekretnineController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Put(2, new Nekretnina { Id = 1, Mesto = "Novi Sad", AgencijskaOznaka = "Nek01", GodinaIzgradnje = 1974, Kvadratura = 50, Cena = 40000, AgentId = 1 });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void PostPretragaReturnsMultipleObjects()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Nekretnina, NekretninaDTO>();
            });
            List<Nekretnina> nekretnine = new List<Nekretnina>();
            nekretnine.Add(new Nekretnina()
            {
                Id = 1,
                Mesto = "Beograd",
                Kvadratura = 67,
                Agent = new Agent() { Id = 1, ImeiPrezime = "Ana Ancic", Licenca = "Lic1", GodinaRodjenja = 1980, BrojProdatihNekretnina = 5 }
            });
            nekretnine.Add(new Nekretnina()
            {
                Id = 2,
                Mesto = "Beograd",
                Kvadratura = 69,
                Agent = new Agent() { Id = 1, ImeiPrezime = "Ana Ancic", Licenca = "Lic1", GodinaRodjenja = 1980, BrojProdatihNekretnina = 5 }
            });

            // Act
            var mockRepository = new Mock<INekretnineRepository>();
            mockRepository.Setup(x => x.Pretraga(55, 80)).Returns(nekretnine.AsQueryable());
            var controller = new NekretnineController(mockRepository.Object);

            // Act
            IQueryable<NekretninaDTO> result = controller.PostPretraga(55, 80);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(nekretnine.Count, result.ToList().Count);

            Assert.AreEqual(nekretnine.ElementAt(0).Id, result.ToList().ElementAt(0).Id);
            Assert.AreEqual(nekretnine.ElementAt(0).Mesto, result.ToList().ElementAt(0).Mesto);
            Assert.AreEqual(nekretnine.ElementAt(0).Kvadratura, result.ToList().ElementAt(0).Kvadratura);
            Assert.AreEqual(nekretnine.ElementAt(0).Agent.ImeiPrezime, result.ToList().ElementAt(0).AgentImeiPrezime);

            Assert.AreEqual(nekretnine.ElementAt(1).Id, result.ToList().ElementAt(1).Id);
            Assert.AreEqual(nekretnine.ElementAt(1).Mesto, result.ToList().ElementAt(1).Mesto);
            Assert.AreEqual(nekretnine.ElementAt(1).Kvadratura, result.ToList().ElementAt(1).Kvadratura);
            Assert.AreEqual(nekretnine.ElementAt(1).Agent.ImeiPrezime, result.ToList().ElementAt(1).AgentImeiPrezime);
        }

    }
}
