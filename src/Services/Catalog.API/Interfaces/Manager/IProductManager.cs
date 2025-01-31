using MongoRepo.Interfaces.Manager;
using Catalog.API.Models;

namespace Catalog.API.Interfaces.Manager
{
    public interface IProductManager : ICommonManager<Product>
    {
        public List<Product> GetByCategory(string category);
    }
}
