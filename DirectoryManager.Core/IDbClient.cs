using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DirectoryManager.Core
{
    public interface IDbClient
    {
        IMongoCollection<Directory> GetDirectoryCollection();
    }
}
