using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Module.User.Application.Domain
{
    public class EntityUser
    {
        public EntityUser()
        {
            ContactDetails = new HashSet<EntityUserContactInformation>();
        }

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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }        
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string CompanyName { get; private set; }
        //public List<EntityUserContactInformation> ContactDetails { get; set; }
        public virtual ICollection<EntityUserContactInformation> ContactDetails { get; set; }
        public bool Active { get; private set; }
        public EntityUser(int id, string firstName, string lastName, string companyName, bool active)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.CompanyName = companyName;
            //this.ContactDetails = new();
            this.Active = active;
        }
        public void setFirstName(string firstName)
        {
            this.FirstName = firstName;
        }
        public void setActive(bool active)
        {
            this.Active = active;
        }


    }

}

