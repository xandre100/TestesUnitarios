using System.Linq;
using tdd.model;

namespace tdd.Repository
{
    public interface IProductRepository
    {
        IQueryable<Product> Add(Product _product);
        IQueryable<Product> GetAll();
        IProduct GetId(int id);
        void Delete(int id);
    }
}