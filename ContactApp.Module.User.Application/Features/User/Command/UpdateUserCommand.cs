using AutoMapper;
using ContactApp.Module.Person.Application.Domain;
using ContactApp.Module.Person.Application.Features.User.Dtos;
using ContactApp.Module.User.Application.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContactApp.Module.Person.Application.Features.User.Command
{

    public partial class UpdateUserCommand : IRequest<UpdateUserDto>
    {
        public string ObjectId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public List<EntityContactInformation> ContactInformations { get; set; }
        //public bool Active { get; set; }

        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserDto>
        {
            private readonly IUserService _UserService;
            private readonly IMapper _mapper;

            public UpdateUserCommandHandler(IUserService UserService, IMapper mapper)
            {
                _UserService = UserService;
                _mapper = mapper;
            }

            public async Task<UpdateUserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                EntityUser mappedPerson = _mapper.Map<EntityUser>(request);
                mappedPerson.Active = true;
                EntityUser createdPerson = _UserService.Save(mappedPerson);
                UpdateUserDto createdPersonDto = _mapper.Map<UpdateUserDto>(createdPerson);

                return createdPersonDto;

            }
        }
    }

}
