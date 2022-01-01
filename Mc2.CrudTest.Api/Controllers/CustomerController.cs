using Mc2.CrudTest.Application.Commands;
using Mc2.CrudTest.Application.Dtos;
using Mc2.CrudTest.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await Mediator.Send(new GetAllCustomersQuery());

            if (response == null || response.Count <= 0)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await Mediator.Send(new GetCustomerByIdQuery(id));

            if (response == null)
                return NotFound();

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> Post(CustomerRequestDto customer)
        {
            var response = await Mediator.Send(new CreateCustomerCommand(customer));

            if (response == null)
                return Conflict();

            return Ok(response);
        }


        [HttpPut]
        public async Task<IActionResult> Put(CustomerRequestDto customer)
        {
            var response = await Mediator.Send(new UpdateCustomerCommand(customer));

            if (response == null)
                return NotFound();

            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await Mediator.Send(new DeleteCustomerCommand(id));

            if (!response)
                return NotFound();
            return Ok();
        }

    }
}
