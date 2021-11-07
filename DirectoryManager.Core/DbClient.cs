
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DirectoryManager.Core
{
    public class DbClient : IDbClient
    {
        private readonly IMongoCollection<Directory> _directories;
        public DbClient(IOptions<DirectoryManagerDbConfig> directoryManagerDbConfig)
        {
            var client = new MongoClient(directoryManagerDbConfig.Value.Connection_String);
            var database = client.GetDatabase(directoryManagerDbConfig.Value.Database_Name);
            _directories = database.GetCollection<Directory>(directoryManagerDbConfig.Value.Directory_Collection_Name);

        }
        public IMongoCollection<Directory> GetDirectoryCollection() => _directories;
    }
}
