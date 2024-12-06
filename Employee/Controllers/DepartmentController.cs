using Employee.Business.Features.Departments.Commands;
using Employee.Business.Features.Departments.Queries;
using Employee.Business.Features.Employees.Commands;
using Employee.Business.Features.Employees.Queries;
using Employee.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Controllers
{
  [ApiController]
  [Route("api/[controller]")]

  public class DepartmentController : ApiMediatBase
  {
    [HttpGet]
    //[ServiceFilter(typeof(ApiKeyAuthFilter))]
    public async Task<IActionResult> Get()
    {
      return Ok(await Mediator.Send(new GetAllDepartmentsQuery()));
    }


    [HttpPost]
    public async Task<DepartmentModel> Create(DepartmentModel departmentModel)
    {
      var command = new CreateDepartmentsCommand(departmentModel);
      return await Mediator.Send(command);
    }
  }
}
