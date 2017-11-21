using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tdd.Repository;
using tdd.web.Controllers;
using Moq;
using System.Linq;
using tdd.model;
using System.Web.Http;
using System.Web.Http.Results;

namespace tdd.web.Tests.Controllers
{
    /// <summary>
    /// Summary description for ProductControllerMockTest
    /// </summary>
    [TestClass]
    public class ProductControllerMockTest
    {
        public ProductControllerMockTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        [TestMethod]
        public void Get_All_Products()
        {
            var mockRepository = new Mock<IProductRepository>();
            
            mockRepository.Setup(r => r.GetAll()).Returns(GetFakeProducts());

            var _controller = new ProductController(mockRepository.Object);

            IHttpActionResult actionResult = _controller.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<IQueryable<Product>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        public void Get_One_Product_Mock()
        {
            var mockRepository = new Mock<IProductRepository>();
            var mockProduct = new Mock<IProduct>();

            mockProduct.Setup(p => p.Descricao).Returns("Bala");
            mockProduct.Setup(p => p.Id).Returns(1);
      
            mockRepository.Setup(r => r.GetId(1)).Returns(mockProduct.Object);

            var _controller = new ProductController(mockRepository.Object);

            IHttpActionResult actionResult = _controller.Get(1);
            var contentResult = actionResult as OkNegotiatedContentResult<IProduct>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.Id);
        }

        [TestMethod]
        public void PostMethodSetsLocationHeader()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var controller = new ProductController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Post(GetFakeProducts().First());            
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod]
        public void GetReturnsNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var controller = new ProductController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Get(10);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        private static IQueryable<Product> GetFakeProducts()
        {
            var fakeProducts = new List<Product>() {
                new Product() { Id = 1, Descricao = "Bala" },
                new Product() { Id = 2, Descricao = "Jujuba" }
            }.AsQueryable();

            return fakeProducts;
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
        
    }
}
