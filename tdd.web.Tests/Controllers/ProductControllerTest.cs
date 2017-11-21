using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tdd.Repository;
using tdd.web.Controllers;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Http.Results;
using tdd.model;
using System.Linq;

namespace tdd.web.Tests.Controllers
{
    /// <summary>
    /// Summary description for ProductsControllerTest
    /// </summary>
    [TestClass]
    public class ProductControllerTest
    {
        IProductRepository _repository;
        ProductController _controller;
        
        [TestInitialize]
        public void Setup()
        {
            _repository = new ProductRepository();
            _controller = new ProductController(_repository);
        }


        public ProductControllerTest()
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
        public void Get_All_Products()
        {
            IHttpActionResult actionResult = _controller.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<IQueryable<Product>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        public void Get_One_Product()
        {
            IHttpActionResult actionResult = _controller.Get(1);
            var contentResult = actionResult as OkNegotiatedContentResult<IProduct>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.Id);

        }
    }
}
