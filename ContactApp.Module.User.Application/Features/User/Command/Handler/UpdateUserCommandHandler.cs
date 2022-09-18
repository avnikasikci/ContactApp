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
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserDto>
    {
        private readonly IUserService _UserService;
        private readonly IUserContactInformationService _UserContactInformationService;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUserService UserService, IUserContactInformationService UserContactInformationService, IMapper mapper)
        {
            _UserService = UserService;
            _UserContactInformationService = UserContactInformationService;            
            _mapper = mapper;
        }

        public async Task<UpdateUserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            EntityUser entityUser = new EntityUser(request.ObjectId, request.FirstName, request.LastName, request.CompanyName, true);

            //EntityUser mappedPerson = _mapper.Map<EntityUser>(request);
            ////mappedPerson.Active = true;
            //mappedPerson.setActive(true);

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
            //List<EntityUserContactInformation> entityContactInformation =await  _UserContactInformationService.SaveSpecial(entityUserContactInformation);

            //request.ContactInformations.ForEach(x => x.ObjectUserId = createdPerson.ObjectId);
            //List<EntityUserContactInformation> entityContactInformation = _UserContactInformationService.SaveSpecial(request.ContactInformations);
            UpdateUserDto createdPersonDto = _mapper.Map<UpdateUserDto>(createdPerson);
            createdPersonDto.ContactInformations = request.ContactInformations;



            return createdPersonDto;

        }
    }

}
