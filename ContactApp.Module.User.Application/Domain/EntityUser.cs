using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactApp.Module.User.Application.Domain
{
    public class EntityUser
    {
        public EntityUser()
        {
            ContactDetails = new HashSet<EntityUserContactInformation>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }        
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string CompanyName { get; private set; }
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

