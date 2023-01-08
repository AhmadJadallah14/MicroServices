using Catalog.Api.Entity;
using MongoDB.Driver;

namespace Catalog.Api.Data
{
    public class CatalogContext : ICatalogContext
    {

        public CatalogContext(IConfiguration configuration)
        {
            // To Create Mongo Connection  With DataBase
            var client =  new MongoClient(configuration.GetValue<string>("DatabaseSettngs:ConnectionString"));
            // get The DataBase and if not exist Will Creaat it 
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettngs:DatabaseName"));
            Products = database.GetCollection<Product>("DatabaseSettngs:CollectionName");
            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
