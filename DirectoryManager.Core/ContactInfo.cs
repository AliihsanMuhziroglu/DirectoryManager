using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DirectoryManager.Core
{

    public class ContactInfo
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }

    }
}
