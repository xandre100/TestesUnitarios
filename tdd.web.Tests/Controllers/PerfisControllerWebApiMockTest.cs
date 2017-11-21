using System;
using System.Text;
using System.Collections.Generic;
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
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class PerfisControllerWebApiMockTest
    {
        public PerfisControllerWebApiMockTest()
        {
            //
            // TODO: Add constructor logic here
            //
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
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Get_All_Profiles()
        {
            var mockRepository = new Mock<IPerfilRepository>();
            var mockController = new Mock<IperfisController>();

            mockRepository = new Mock<IPerfilRepository>();
            mockRepository.Setup(m => m.Getperfis()).Returns(GetFakeProfiles());

            mockController.Setup(c => c.Getperfis()).Returns(mockRepository.Object.Getperfis);

            var _controller = new perfisController(mockController.Object, mockRepository.Object);
            IQueryable<perfis> contentResult = _controller.Getperfis();

            // Assert
            Assert.IsNotNull(contentResult);
        }

        [TestMethod]
        public void Get_One_Profile()
        {
            var mockRepository = new Mock<IPerfilRepository>();
            var mockController = new Mock<IperfisController>();
            var _perfil = new perfis() { id_perfil = 1, descricao = "Biscoito", ativo = 1, usuarios = new List<usuarios>() };

            mockRepository.Setup(m => m.Getperfis(It.IsAny<int>())).Returns(_perfil);

            mockController.Setup(c => c.Getperfis(It.IsAny<int>()));
            
            var _controller = new perfisController(mockController.Object, mockRepository.Object);
            
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
            var mockController = new Mock<IperfisController>();

            var _perfil = new perfis() { id_perfil = 3, descricao = "Biscoito", ativo = 1, usuarios = new List<usuarios>() };

            mockRepository.Setup(m => m.Putperfis(It.IsAny<int>(), It.IsAny<perfis>()));

            mockController.Setup(c => c.Putperfis(It.IsAny<int>(), It.IsAny<perfis>()));

            var _controller = new perfisController(mockController.Object, mockRepository.Object);

            IHttpActionResult actionResult = _controller.Putperfis(3, _perfil);
            var contentResult = actionResult as StatusCodeResult;

            Assert.IsNotNull(contentResult);
            Assert.AreEqual(contentResult.StatusCode, System.Net.HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void Delete_One_Profile()
        {
            var mockRepository = new Mock<IPerfilRepository>();
            var mockController = new Mock<IperfisController>();

            var _perfil = new perfis() { id_perfil = 3, descricao = "Biscoito", ativo = 1, usuarios = new List<usuarios>() };

            mockRepository.Setup(m => m.Getperfis(It.IsAny<int>())).Returns(_perfil);
            mockRepository.Setup(m => m.Deleteperfis(It.IsAny<int>())).Returns(_perfil);

            var _controller = new perfisController(mockController.Object, mockRepository.Object);

            IHttpActionResult actionResult = _controller.Deleteperfis(3);
            var contentResult = actionResult as OkNegotiatedContentResult<perfis>;
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(contentResult.Content.id_perfil, 3);
        }

        [TestMethod]
        public void Delete_One_Profile_NotFound()
        {
            var mockRepository = new Mock<IPerfilRepository>();
            var mockController = new Mock<IperfisController>();

            var _perfil = new perfis() { id_perfil = 3, descricao = "Biscoito", ativo = 1, usuarios = new List<usuarios>() };
            
            mockRepository.Setup(m => m.Getperfis(4)).Returns(_perfil);
            mockRepository.Setup(m => m.Deleteperfis(It.IsAny<int>())).Returns(_perfil);

            var _controller = new perfisController(mockController.Object, mockRepository.Object);

            IHttpActionResult actionResult = _controller.Deleteperfis(3);
            var contentResult = actionResult as NotFoundResult;
            Assert.IsNotNull(contentResult);
            Assert.IsInstanceOfType(contentResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Post_One_Profile()
        {
            var mockRepository = new Mock<IPerfilRepository>();
            var mockController = new Mock<IperfisController>();
            var _perfil = new perfis() { id_perfil = 3, descricao = "Biscoito", ativo = 1, usuarios = new List<usuarios>() };

            mockRepository.Setup(m => m.Postperfis(It.IsAny<perfis>()));
            mockController.Setup(m => m.Postperfis(It.IsAny<perfis>()));

            var _controller = new perfisController(mockController.Object, mockRepository.Object);

            IHttpActionResult actionResult = _controller.Postperfis(_perfil);
            var contentResult = actionResult as CreatedAtRouteNegotiatedContentResult<perfis>;
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(contentResult.Content.id_perfil, 3);
        }

        private static IQueryable<perfis> GetFakeProfiles()
        {
            var fakeProfiles = new List<perfis>() {
                new perfis() { id_perfil = 1, descricao = "Bala", ativo = 1, usuarios = new List<usuarios>()  },
                new perfis() { id_perfil = 2, descricao = "Jujuba", ativo = 1, usuarios = new List<usuarios>() }
            }.AsQueryable();

            return fakeProfiles;
        }
    }
}
