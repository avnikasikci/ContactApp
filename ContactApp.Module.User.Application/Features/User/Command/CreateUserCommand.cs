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

    public partial class CreateUserCommand : IRequest<CreatedUserDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public List<EntityContactInformation> ContactInformations { get; set; }
        //public bool Active { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreatedUserDto>
        {
            private readonly IUserService _UserService;
            private readonly IMapper _mapper;

            public CreateUserCommandHandler(IUserService UserService, IMapper mapper)
            {
                _UserService = UserService;
                _mapper = mapper;
            }

            public async Task<CreatedUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                EntityUser mappedPerson = _mapper.Map<EntityUser>(request);
                mappedPerson.Active = true;
                EntityUser createdPerson = _UserService.Save(mappedPerson);
                CreatedUserDto createdPersonDto = _mapper.Map<CreatedUserDto>(createdPerson);

                return createdPersonDto;

            }
        }
    }

}
