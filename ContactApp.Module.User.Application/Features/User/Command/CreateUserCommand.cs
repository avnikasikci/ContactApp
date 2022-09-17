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

namespace ContactApp.Module.User.Application.Features.User.Command
{

    public partial class CreateUserCommand : IRequest<CreatedUserDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public List<UserContactInformationDto> ContactInformations { get; set; }
        //public bool Active { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreatedUserDto>
        {
            private readonly IUserService _UserService;
            private readonly IUserContactInformationService _UserContactInformationService;
            private readonly IMapper _mapper;

            public CreateUserCommandHandler(IUserService UserService, IUserContactInformationService UserContactInformationService, IMapper mapper)
            {
                _UserService = UserService;
                _UserContactInformationService = UserContactInformationService;
                _mapper = mapper;
            }

            public async Task<CreatedUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                EntityUser entityUser = new EntityUser("", request.FirstName, request.LastName, request.CompanyName, true);
                //EntityUser mappedPerson = _mapper.Map<EntityUser>(request);
                //mappedPerson.Active = true;
                //entityUser.setActive(true);
                EntityUser createdPerson = _UserService.Save(entityUser);

                List<EntityUserContactInformation> entityUserContactInformation = (from m in request.ContactInformations
                                                                                   select new EntityUserContactInformation
                                                                                   {
                                                                                       InformationDesc = m.InformationDesc,
                                                                                       InformationType = m.InformationType,
                                                                                       ObjectUserId = createdPerson.ObjectId,

                                                                                   }).ToList();

                entityUserContactInformation.ForEach(x => x.ObjectUserId = createdPerson.ObjectId);
                await _UserContactInformationService.SaveSpecial(entityUserContactInformation);
                //List<EntityUserContactInformation> entityContactInformation = await _UserContactInformationService.SaveSpecial(entityUserContactInformation);
                CreatedUserDto createdPersonDto = _mapper.Map<CreatedUserDto>(createdPerson);
                createdPersonDto.ContactInformations = request.ContactInformations;
                return createdPersonDto;

            }
        }
    }

}
