using System;
using System.Collections.Generic;

namespace DirectoryManager.Core
{
    public class DirectoryServices : IDirectoryServices
    {
        public List<Directory> GetDirectories()
        {
            return new List<Directory>
            {
                new Directory{Name = "test", Company = "setur"}
            };
        }
    }
}
