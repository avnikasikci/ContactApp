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
        private readonly IUserService _UserService;
        private readonly IMapper _mapper;

        public DeleteUserCommandHandler(IUserService UserService, IMapper mapper)
        {
            _UserService = UserService;
            _mapper = mapper;
        }

        public async Task<string> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var Entity = _UserService.SelectById(request.ObjectId);
            if (Entity != null)
            {
                //Entity.Active = false;
                Entity.setActive(false);

                _UserService.Save(Entity);
                return Entity.ObjectId;

            }
            else
            {
                return "Not Found User";
            }

        }
    }
}
