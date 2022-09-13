using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Module.User.Application.Domain
{
    public class EntityUser
    {
        //public EntityUser()
        //{
        //    ContactInformations = new Collection<EntityContactInformation>();
        //}
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ObjectId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        //[NotMapped]
        //public List<EntityContactInformation> ContactInformations { get; set; }
        //public virtual ICollection<EntityContactInformation> ContactInformations { get; set; }

        public bool Active { get; set; }
    }

}

