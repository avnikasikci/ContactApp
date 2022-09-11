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
            GetListPersonQuery getListPersonQuery = new() { };
            List<PersonListDto> result = await this.Mediator.Send(getListPersonQuery);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreatePersonCommand createPersonCommand)
        {
            CreatedPersonDto result = await Mediator.Send(createPersonCommand);
            return Created("", result);
        }
    }
}
