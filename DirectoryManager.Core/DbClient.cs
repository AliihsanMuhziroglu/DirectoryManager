
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DirectoryManager.Core
{
    public class DbClient : IDbClient
    {
        private readonly IMongoCollection<Directory> _directories;
        public DbClient(IOptions<DirectoryManagerDbConfig> directoryManagerDbConfig)
        {
            var client = new MongoClient("mongodb+srv://aliihsanmuhziroglu:ali123@cluster0.vz9ol.mongodb.net/DirectoryManagerDb?retryWrites=true&w=majority");
            var database = client.GetDatabase(directoryManagerDbConfig.Value.Database_Name);
            _directories = database.GetCollection<Directory>(directoryManagerDbConfig.Value.Directory_Collection_Name);

        }
        public IMongoCollection<Directory> GetDirectoryCollection() => _directories;
    }
}
