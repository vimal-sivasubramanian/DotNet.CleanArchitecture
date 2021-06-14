using DotNet.CleanArchitecture.Service.Application.Persons.Commands.CreatePerson;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DotNet.CleanArchitecture.Service.Api.Controllers
{
    public class PersonsController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Create(CreatePersonCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }
    }
}
