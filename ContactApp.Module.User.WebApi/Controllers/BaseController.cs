using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactApp.Module.User.WebApi.Controllers
{
    public class BaseController : ControllerBase
    {
        //protected IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService(typeof(IMediator)) as IMediator;

        private IMediator? _mediator;


    }
}
