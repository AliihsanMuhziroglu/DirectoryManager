using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace DirectoryManager.Core
{
    public class DirectoryServices : IDirectoryServices
    {
        private readonly IMongoCollection<Directory> _directories;
        public DirectoryServices(IDbClient dbClient)
        {
            _directories = dbClient.GetDirectoryCollection();
        }
        public List<Directory> GetDirectories() => _directories.Find(directory => true).ToList();

    }
}
