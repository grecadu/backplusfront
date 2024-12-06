using AutoMapper;
using Employee.Business.Common;
using Employee.Business.Interfaces;
using Employee.Business.Models;
using Employee.Common.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Business.Features.Employees.Commands
{
  public record UpdateEmployeesCommand(EmployeesModel Model) : IRequest<int>;

  public class UpdateEmployeesCommandHandler : BaseCommandHandler, IRequestHandler<UpdateEmployeesCommand, int>
  {
    public UpdateEmployeesCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

    public async Task<int> Handle(UpdateEmployeesCommand request, CancellationToken cancellationToken)
    {

      var employee = await UnitOfWork.Repository<EmployeeEntity>().GetByIdAsync(request.Model.Id)
          ?? throw new Exception($"Province with id {request.Model.Id}");

      employee.Phone = request.Model.Phone;
      employee.LastName = request.Model.LastName;
      employee.FirstName = request.Model.FirstName; 
      employee.HireDate = request.Model.HireDate;
      employee.DepartmentId = request.Model.DepartmentId;
      employee.Address = request.Model.Address;

      await UnitOfWork.Repository<EmployeeEntity>().UpdateAsync(employee);

      return await UnitOfWork.Save<EmployeeEntity>(cancellationToken);
    }
  }
}
