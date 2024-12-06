using Employee.Business.Features.Employees.Commands;
using Employee.Business.Features.Employees.Queries;
using Employee.Business.Models;
using Employee.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ApiMediatBase
{
  [HttpGet]
  //[ServiceFilter(typeof(ApiKeyAuthFilter))]
  public async Task<IActionResult> Get()
  {
    return Ok(await Mediator.Send(new GetAllEmployeesQuery()));
  }

  [HttpPost]
  
  public async Task<EmployeesModel> Create(EmployeesModel employee)
  {
    var command = new CreateEmployeesCommand(employee);
    return await Mediator.Send(command);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> Update(Guid id, EmployeesModel employee)
  {
    if (employee.Id != id)
    {
      return BadRequest();
    }
    var command = new UpdateEmployeesCommand(employee);
    return Ok(await Mediator.Send(command));
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetByEmployeeId(Guid id)
  {
    
    var command = new GetEmployeeByIdQuery(id);
    return Ok(await Mediator.Send(command));
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(Guid id)
  {

    var command = new DeleteEmployeesCommand(id);
    return Ok(await Mediator.Send(command));
  }
}
