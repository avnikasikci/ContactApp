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
        public List<UserContactInformationDto> ContactInformations { get; set; }
        //public bool Active { get; set; }

        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserDto>
        {
            private readonly IUserService _UserService;
            private readonly IUserContactInformationService _UserContactInformationService;
            private readonly IMapper _mapper;

            public UpdateUserCommandHandler(IUserService UserService, IUserContactInformationService UserContactInformationService, IMapper mapper)
            {
                _UserContactInformationService = UserContactInformationService;
                _UserService = UserService;
                _mapper = mapper;
            }

            public async Task<UpdateUserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                EntityUser mappedPerson = _mapper.Map<EntityUser>(request);
                mappedPerson.Active = true;
                EntityUser createdPerson = _UserService.Save(mappedPerson);
                List<EntityUserContactInformation> entityUserContactInformation = (from m in request.ContactInformations
                                                                                   select new EntityUserContactInformation
                                                                                   {
                                                                                       InformationDesc = m.InformationDesc,
                                                                                       InformationType = m.InformationType,
                                                                                       ObjectUserId = createdPerson.ObjectId,

                                                                                   }).ToList();

                entityUserContactInformation.ForEach(x => x.ObjectUserId = createdPerson.ObjectId);
                await _UserContactInformationService.SaveSpecial(entityUserContactInformation);
                //List<EntityUserContactInformation> entityContactInformation =await  _UserContactInformationService.SaveSpecial(entityUserContactInformation);

                //request.ContactInformations.ForEach(x => x.ObjectUserId = createdPerson.ObjectId);
                //List<EntityUserContactInformation> entityContactInformation = _UserContactInformationService.SaveSpecial(request.ContactInformations);
                UpdateUserDto createdPersonDto = _mapper.Map<UpdateUserDto>(createdPerson);
                createdPersonDto.ContactInformations = request.ContactInformations;



                return createdPersonDto;

            }
        }
    }

}
