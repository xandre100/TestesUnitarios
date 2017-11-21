using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using tdd.model;
using tdd.Repository;

namespace tdd.web.Controllers
{
    public class ProductController : ApiController
    {
        IProductRepository _productRepository;

        public ProductController()
        {
            if (this._productRepository == null)
                this._productRepository = new ProductRepository();

        }

        public ProductController(IProductRepository _repository)
        {
            this._productRepository = _repository;
        }

        // GET: api/Product
        public IHttpActionResult Get()
        {
            var products = _productRepository.GetAll();
            return Ok(products);
        }

        // GET: api/Product/5
        public IHttpActionResult Get(int id)
        {
            IProduct product = _productRepository.GetId(id);

            if (product == null)
                return NotFound();
            
            return Ok(product);
        }

        // POST: api/Product
        public IHttpActionResult Post(Product product)
        {
            _productRepository.Add(product);
            return Ok();
        }

        // PUT: api/Product/5
        public void Put(int id, [FromBody]string value)
        {
            var dados = id + string.Format("valor Preenchido: {0}", value);
        }

        // DELETE: api/Product/5
        public IHttpActionResult Delete(int id)
        {
            _productRepository.Delete(id);
            return Ok();
        }

        public String ReturnString(string teste)
        {
            return string.Format("Você digitou {0}", teste);
        }
    }
}
