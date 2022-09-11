﻿using ContactApp.Module.Person.Application.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Module.Person.Application.Features.User.Dtos
{
    public class UserDto
    {
        public string ObjectId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public List<EntityContactInformation> ContactInformations { get; set; }
        public bool Active { get; set; }
    }
}
