using MongoRepo.Repository;
using Catalog.API.Models;
using Catalog.API.Interfaces.Manager;
using Catalog.API.Context;
using Catalog.API.Repository;
using MongoRepo.Manager;

namespace Catalog.API.Manager
{
        public class ProductManager: CommonManager<Product>, IProductManager
        {
            public ProductManager() : base(new ProductRepository())
            {
            }
        }

}
