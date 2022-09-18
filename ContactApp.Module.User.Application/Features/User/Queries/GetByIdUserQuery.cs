using AutoMapper;
using ContactApp.Module.User.Application.Domain;
using ContactApp.Module.User.Application.Features.User.Dtos;
using ContactApp.Module.User.Application.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContactApp.Module.User.Application.Features.User.Queries
{

    public class GetByIdUserQuery : IRequest<UserDto>
    {
        //public PageRequest PageRequest { get; set; }
        public string ObjectId { get; set; }
  
    }

}
