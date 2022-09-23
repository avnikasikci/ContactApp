using AutoMapper;
using ContactApp.Module.User.Application.Domain;
using ContactApp.Module.User.Application.Features.User.Command;
using ContactApp.Module.User.Application.Features.User.Dtos;
using ContactApp.Module.User.Application.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContactApp.Module.User.Application.Features.User.Queries.Handler
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreatedUserDto>
    {
        private readonly IUserService _userService;
        private readonly IUserContactInformationService _userContactInformationService;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserService userService, IUserContactInformationService userContactInformationService, IMapper mapper)
        {
            _userService = userService;
            _userContactInformationService = userContactInformationService;
            _mapper = mapper;
        }

        public async Task<CreatedUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            EntityUser entityUser = new EntityUser(0, request.FirstName, request.LastName, request.CompanyName, true);
            //EntityUser mappedPerson = _mapper.Map<EntityUser>(request);
            //mappedPerson.Active = true;
            //entityUser.setActive(true);
            EntityUser createdPerson = _userService.Add(entityUser);

            List<EntityUserContactInformation> entityUserContactInformation = (from m in request.ContactInformations
                                                                               select new EntityUserContactInformation
                                                                               {
                                                                                   //Id=m.,
                                                                                   InformationDesc = m.InformationDesc,
                                                                                   InformationType = m.InformationType,
                                                                                   UserId = createdPerson.Id,

                                                                               }).ToList();

            entityUserContactInformation.ForEach(x => x.UserId = createdPerson.Id);
            await _userContactInformationService.SaveSpecial(entityUserContactInformation);
            //List<EntityUserContactInformation> entityContactInformation = await _UserContactInformationService.SaveSpecial(entityUserContactInformation);
            CreatedUserDto createdPersonDto = _mapper.Map<CreatedUserDto>(createdPerson);
            createdPersonDto.ContactInformations = request.ContactInformations;
            return createdPersonDto;

        }
    }
}
