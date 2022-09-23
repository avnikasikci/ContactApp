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
        private readonly IUserService _userService;
        private readonly IUserContactInformationService _userContactInformationService;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUserService userService, IUserContactInformationService userContactInformationService, IMapper mapper)
        {
            _userService = userService;
            _userContactInformationService = userContactInformationService;
            _mapper = mapper;
        }

        public async Task<UpdateUserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            int Id = 0;
            int.TryParse(request.Id, out Id);
            EntityUser entityUser = new EntityUser(Id ,request.FirstName, request.LastName, request.CompanyName, true);

            EntityUser createdPerson = _userService.Save(entityUser);
            List<EntityUserContactInformation> entityUserContactInformation = (from m in request.ContactInformations
                                                                               select new EntityUserContactInformation
                                                                               {
                                                                                   InformationDesc = m.InformationDesc,
                                                                                   InformationType = m.InformationType,
                                                                                   UserId = createdPerson.Id,

                                                                               }).ToList();

            entityUserContactInformation.ForEach(x => x.UserId = createdPerson.Id);
            await _userContactInformationService.SaveSpecial(entityUserContactInformation);            
            UpdateUserDto createdPersonDto = _mapper.Map<UpdateUserDto>(createdPerson);
            createdPersonDto.ContactInformations = request.ContactInformations;

            return createdPersonDto;

        }
    }

}
