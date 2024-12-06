using AutoMapper;
using Employee.Business.Common;
using Employee.Business.Interfaces;
using Employee.Common.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Business.Features.Employees.Commands
{
  public record DeleteEmployeesCommand(Guid id) : IRequest<int>;

  public class DeleteEmployeesCommandHandler : BaseCommandHandler, IRequestHandler<DeleteEmployeesCommand, int>
  {
    public DeleteEmployeesCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

    public async Task<int> Handle(DeleteEmployeesCommand request, CancellationToken cancellationToken)
    {
      var employee = await UnitOfWork.Repository<EmployeeEntity>().GetByIdAsync(request.id)
          ?? throw new Exception($"Employee with id {request.id} not found.");

      await UnitOfWork.Repository<EmployeeEntity>().DeleteAsync(employee);

      return await UnitOfWork.Save<EmployeeEntity>(cancellationToken);
    }
  }

}
