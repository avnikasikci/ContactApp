using AutoMapper;
using ContactApp.Module.User.Application.Domain;
using ContactApp.Module.User.Application.Features.User.Command;
using ContactApp.Module.User.Application.Features.User.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Module.User.Application.Features.User.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            
            CreateMap<EntityUser,UserDto>().ReverseMap();
            CreateMap<EntityUser, CreatedUserDto>().ReverseMap();
            CreateMap<EntityUser, UpdateUserDto>().ReverseMap();
            CreateMap<EntityUser, CreateUserCommand>().ReverseMap();
            CreateMap<EntityUser, UpdateUserCommand>().ReverseMap();
            
        }
    }

}
