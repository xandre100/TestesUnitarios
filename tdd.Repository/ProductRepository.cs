using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tdd.model;

namespace tdd.Repository
{
    public class ProductRepository : IProductRepository
    {
        public Product Product { get; set; }
        public IQueryable<Product> Products { get; set; }

        public ProductRepository()
        {}

        public ProductRepository(Product _product)
        {
            this.Product = _product;
        }

        public IQueryable<Product> GetAll()
        {
            var products = new List<Product>() {
                new Product() { Id = 1, Descricao = "Bala" },
                new Product() { Id = 2, Descricao = "Jujuba" }
            };

            return products.AsQueryable();
        }

        public IQueryable<Product> Add(Product _product)
        {
            var products = new List<Product>() { _product };
            return products.AsQueryable();
        }

        public IProduct GetId(int id)
        {
            var products = GetAll();

            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return null;

            return product;
        }

        public void Delete(int id)
        {
            List<Product> products = GetAll().ToList();

            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                throw new Exception();

            products.Remove(product);
            
        }
    }
}
