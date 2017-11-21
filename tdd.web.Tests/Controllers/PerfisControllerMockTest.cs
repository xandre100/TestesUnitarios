using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using tdd.web.Controllers;
using System.Linq;
using System.Web.Http;
using tdd.model.entity;
using System.Web.Http.Results;

namespace tdd.web.Tests.Controllers
{
    /// <summary>
    /// Summary description for PerfisControllerTest
    /// </summary>
    [TestClass]
    public class PerfisControllerMockTest
    {


        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize() {

        }

        public PerfisControllerMockTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static IQueryable<perfis> GetFakeProfiles()
        {
            var fakeProfiles = new List<perfis>() {
                new perfis() { id_perfil = 1, descricao = "Bala", ativo = 1, usuarios = new List<usuarios>()  },
                new perfis() { id_perfil = 2, descricao = "Jujuba", ativo = 1, usuarios = new List<usuarios>() }
            }.AsQueryable();

            return fakeProfiles;
        }


        [TestMethod]
        public void Get_All_Profiles()
        {
            var mockRepository = new Mock<IPerfilRepository>();

            mockRepository = new Mock<IPerfilRepository>();
            mockRepository.Setup(m => m.Getperfis()).Returns(GetFakeProfiles());

            var _controller = new perfisController(mockRepository.Object);
            IQueryable<perfis> contentResult = _controller.Getperfis();
            
            // Assert
            Assert.IsNotNull(contentResult);
        }

        [TestMethod]
        public void Get_One_Profile()
        {
            var mockRepository = new Mock<IPerfilRepository>();
            var _perfil = new perfis() { id_perfil = 1, descricao = "Biscoito", ativo = 1, usuarios = new List<usuarios>() };
            mockRepository.Setup(m => m.Getperfis(1)).Returns(_perfil);

            var _controller = new perfisController(mockRepository.Object);

            IHttpActionResult actionResult = _controller.Getperfis(1);
            var contentResult = actionResult as OkNegotiatedContentResult<perfis>;

            Assert.IsNotNull(contentResult);
            Assert.IsInstanceOfType(contentResult.Content, typeof(perfis));
            Assert.AreEqual(contentResult.Content.id_perfil, 1);
        }


        [TestMethod]
        public void Put_One_Profile()
        {
            var mockRepository = new Mock<IPerfilRepository>();
            var _perfil = new perfis() { id_perfil = 3, descricao = "Biscoito", ativo = 1, usuarios = new List<usuarios>() };
            mockRepository.Setup(m => m.Putperfis(3, _perfil));

            var _controller = new perfisController(mockRepository.Object);

            IHttpActionResult actionResult = _controller.Putperfis(3, _perfil);
            var contentResult = actionResult as StatusCodeResult;
            
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(contentResult.StatusCode, System.Net.HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void Post_One_Profile()
        {
            var mockRepository = new Mock<IPerfilRepository>();
            var _perfil = new perfis() { id_perfil = 3, descricao = "Biscoito", ativo = 1, usuarios = new List<usuarios>() };
            mockRepository.Setup(m => m.Postperfis(_perfil));

            var _controller = new perfisController(mockRepository.Object);

            IHttpActionResult actionResult = _controller.Postperfis(_perfil);
            var contentResult = actionResult as CreatedAtRouteNegotiatedContentResult<perfis>;
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(contentResult.Content.id_perfil, 3);
        }

        [TestMethod]
        public void Delete_One_Profile()
        {
            var mockRepository = new Mock<IPerfilRepository>();
            var _perfil = new perfis() { id_perfil = 3, descricao = "Biscoito", ativo = 1, usuarios = new List<usuarios>() };

            mockRepository.Setup(m => m.Getperfis(3)).Returns(_perfil);
            mockRepository.Setup(m => m.Deleteperfis(3)).Returns(_perfil);

            var _controller = new perfisController(mockRepository.Object);

            IHttpActionResult actionResult = _controller.Deleteperfis(3);
            var contentResult = actionResult as OkNegotiatedContentResult<perfis>;
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(contentResult.Content.id_perfil, 3);
        }

        [TestMethod]
        public void Delete_One_Profile_NotFound()
        {
            var mockRepository = new Mock<IPerfilRepository>();
            var _perfil = new perfis() { id_perfil = 3, descricao = "Biscoito", ativo = 1, usuarios = new List<usuarios>() };

            mockRepository.Setup(m => m.Getperfis(4)).Returns(_perfil);
            mockRepository.Setup(m => m.Deleteperfis(3)).Returns(_perfil);

            var _controller = new perfisController(mockRepository.Object);

            IHttpActionResult actionResult = _controller.Deleteperfis(3);
            var contentResult = actionResult as NotFoundResult;
            Assert.IsNotNull(contentResult);
            Assert.IsInstanceOfType(contentResult, typeof(NotFoundResult));
        }


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        
    }
}
