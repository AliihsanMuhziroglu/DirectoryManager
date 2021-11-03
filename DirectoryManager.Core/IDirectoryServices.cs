using System;
using System.Collections.Generic;
using System.Text;

namespace DirectoryManager.Core
{
    interface IDirectoryServices
    {
        List<Directory> GetDirectories();
    }
}
