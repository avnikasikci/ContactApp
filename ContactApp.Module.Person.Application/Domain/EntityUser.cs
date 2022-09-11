using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Module.Person.Application.Domain
{
    public class EntityUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ObjectId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public List<EntityContactInformation> ContactInformations { get; set; }
        public bool Active { get; set; }
    }
    public class EntityContactInformation
    {
        public int InformationType { get; set; }
        public string InformationDesc { get; set; }
    }
}
}
