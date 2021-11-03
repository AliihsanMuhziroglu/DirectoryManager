using System;
using System.Collections.Generic;
using System.Text;

namespace DirectoryManager.Core
{
    public interface IDirectoryServices
    {
        List<Directory> GetDirectories();
    }
}
