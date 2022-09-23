using AutoMapper;
using ContactApp.Core.Application.SharedModels;
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
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, string>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public DeleteUserCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<string> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var Entity = _userService.SelectById(request.Id);
            if (Entity != null)
            {
                //Entity.Active = false;
                Entity.setActive(false);

                _userService.Save(Entity);
                return Entity.Id.ToString();

            }
            else
            {
                return "Not Found User";
            }

        }
    }
}
