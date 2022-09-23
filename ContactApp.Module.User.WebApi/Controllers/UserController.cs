
using ContactApp.Module.User.Application.Features.User.Command;
using ContactApp.Module.User.Application.Features.User.Dtos;
using ContactApp.Module.User.Application.Features.User.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactApp.Module.User.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            GetListUserQuery getListPersonQuery = new() { };
            List<UserDto> result = await this.Mediator.Send(getListPersonQuery);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateUserCommand createUserCommand)
        {
            CreatedUserDto result = await Mediator.Send(createUserCommand);
            return Created("", result);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand updateUserCommand)
        {
            UpdateUserDto result = await Mediator.Send(updateUserCommand);
            return Created("", result);
        }
        //[HttpDelete]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteUserCommand deleteUserCommand)
        {
            string result = await Mediator.Send(deleteUserCommand);
            return Created("", result);
        }
        [HttpGet("{ObjectId}")]
        public async Task<IActionResult> GetById(string ObjectId)
        {
            GetByIdUserQuery getByIdUserQuery = new() { id = ObjectId };

            UserDto result = await Mediator.Send(getByIdUserQuery);
            return Created("", result);
        }
    }
}
