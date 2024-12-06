using AutoMapper;
using Employee.Business.Common;
using Employee.Business.Features.Employees.Commands;
using Employee.Business.Interfaces;
using Employee.Business.Models;
using Employee.Common.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Business.Features.Departments.Commands
{
  public record CreateDepartmentsCommand(DepartmentModel Model) : IRequest<DepartmentModel>;

  public class CreateDeparmentsCommandHandler : BaseCommandHandler, IRequestHandler<CreateDepartmentsCommand, DepartmentModel>
  {
    public CreateDeparmentsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

    public async Task<DepartmentModel> Handle(CreateDepartmentsCommand request, CancellationToken cancellationToken)
    {
      var department = Mapper.Map<DepartmentEntity>(request.Model);

      _ = await UnitOfWork.Repository<DepartmentEntity>().AddAsync(department);
      _ = await UnitOfWork.Save<DepartmentEntity>(cancellationToken);
      return Mapper.Map<DepartmentModel>(department);
    }

  }
}
