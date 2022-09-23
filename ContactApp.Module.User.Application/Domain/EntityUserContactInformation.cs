using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactApp.Module.User.Application.Domain
{
    public class EntityUserContactInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual EntityUser User { get; set; }
        public int InformationType { get; set; }
        public string InformationDesc { get; set; }
    }
}

