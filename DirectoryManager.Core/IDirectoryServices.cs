using System;
using System.Collections.Generic;
using System.Text;

namespace DirectoryManager.Core
{
    public interface IDirectoryServices
    {
        List<Directory> GetDirectories();
        Directory GetDirectory(string id);
        Directory AddDirectory(Directory directory);


    }
}
