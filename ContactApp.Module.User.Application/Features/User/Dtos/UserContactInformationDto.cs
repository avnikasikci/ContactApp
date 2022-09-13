using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Module.User.Application.Domain
{    public class UserContactInformationDto
    {
        public int InformationType { get; set; }
        public string InformationDesc { get; set; }
    }
}

