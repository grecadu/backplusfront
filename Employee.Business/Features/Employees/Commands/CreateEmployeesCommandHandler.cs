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

namespace Employee.Business.Features.Employees.Commands;

public record CreateEmployeesCommand(EmployeesModel Model) : IRequest<EmployeesModel>;

public class CreateEmployeesCommandHandler : BaseCommandHandler, IRequestHandler<CreateEmployeesCommand, EmployeesModel>
{
    public CreateEmployeesCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

  public async Task<EmployeesModel> Handle(CreateEmployeesCommand request, CancellationToken cancellationToken)
  {
    var employee = Mapper.Map<EmployeeEntity>(request.Model);
    
    _ = await UnitOfWork.Repository<EmployeeEntity>().AddAsync(employee);
    _ = await UnitOfWork.Save<EmployeeEntity>(cancellationToken);
    return Mapper.Map<EmployeesModel>(employee);
  }
}
