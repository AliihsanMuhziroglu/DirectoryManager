using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryManager.UI.Models
{
    public class CreateContactInfoModel
    {
        public DirectoryModel directory { get; set; }
        public ContactInfoModel contactInfo { get; set; }
    }
}
