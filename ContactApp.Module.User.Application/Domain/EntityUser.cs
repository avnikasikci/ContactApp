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
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        //public string ObjectId { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string CompanyName { get; set; }
        //[NotMapped]
        //public List<EntityContactInformation> ContactInformations { get; set; }
        //public virtual ICollection<EntityContactInformation> ContactInformations { get; set; }

        //public bool Active { get; set; }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ObjectId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string CompanyName { get; private set; }

        public bool Active { get; private set; }
        public EntityUser(string objectId, string firstName, string lastName, string companyName, bool active)
        {
            this.ObjectId = objectId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.CompanyName = companyName;   
            this.Active = active;
        }
        public void setActive(bool active)
        {
            this.Active = active;
        }


    }

}

