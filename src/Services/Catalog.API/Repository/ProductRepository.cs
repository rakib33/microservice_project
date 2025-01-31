﻿using MongoRepo.Repository;
using Catalog.API.Models;
using Catalog.API.Interfaces.Repository;
using Catalog.API.Context;

namespace Catalog.API.Repository
{
    public class ProductRepository : CommonRepository<Product>, IProductRepository
    {
        public ProductRepository() : base(new CatalogDbContext())
        {
        }
    }
}
