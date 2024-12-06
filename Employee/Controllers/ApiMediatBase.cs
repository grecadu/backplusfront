using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Controllers;

public class ApiMediatBase : ControllerBase
{
  protected IMediator Mediator => HttpContext.RequestServices.GetService<IMediator>()
                                   ?? throw new Exception("MediatoR is not registered as a service.");
}
