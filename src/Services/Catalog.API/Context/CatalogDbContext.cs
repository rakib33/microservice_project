﻿using MongoRepo.Context;

namespace Catalog.API.Context
{
    public class CatalogDbContext : ApplicationDbContext
    {
        static IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true).Build();
        //static string connectionString = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json",true,true).Build().GetConnectionString("Catalog.API");

        static string connectionString = Configuration.GetConnectionString("Catalog.API");
        static string databaseName =  Configuration.GetValue<string>("DatabaseName");

        public CatalogDbContext() : base(connectionString, databaseName)
        {

        }
    }
}
