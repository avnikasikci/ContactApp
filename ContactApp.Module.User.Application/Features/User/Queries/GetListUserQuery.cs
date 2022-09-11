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

namespace ContactApp.Module.Person.Application.Features.User.Queries
{

    public class GetListUserQuery : IRequest<List<UserDto>>
    {
        //public PageRequest PageRequest { get; set; }
        public class GetListUserQueryHandler : IRequestHandler<GetListUserQuery, List<UserDto>>
        {
            private readonly IUserService _userService;
            private readonly IMapper _mapper;

            public GetListUserQueryHandler(IUserService userService, IMapper mapper)
            {
                _userService = userService;
                _mapper = mapper;
            }

            public async Task<List<UserDto>> Handle(GetListUserQuery request, CancellationToken cancellationToken)
            {
                List<EntityUser> personList = _userService.GetAll().ToList();
                var mappedPersonListModel = (from m in personList
                                             select new UserDto
                                             {
                                                 ObjectId = m.ObjectId,
                                                 Active = m.Active,
                                                 CompanyName = m.CompanyName,
                                                 FirstName = m.FirstName,
                                                 LastName = m.LastName,
                                                 ContactInformations = m.ContactInformations,
                                             }).ToList();

                return mappedPersonListModel;
            }
        }
    }

}
