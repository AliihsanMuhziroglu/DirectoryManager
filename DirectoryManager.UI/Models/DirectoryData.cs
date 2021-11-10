using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryManager.UI.Models
{
    public class DirectoryData
    {
        public string UUID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public List<ContactInfoData> ContactList { get; set; }
    }
}
