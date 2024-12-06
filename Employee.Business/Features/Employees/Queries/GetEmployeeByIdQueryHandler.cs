using AutoMapper;
using Employee.Business.Common;
using Employee.Business.Interfaces;
using Employee.Business.Models;
using Employee.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Business.Features.Employees.Queries;

public record GetEmployeeByIdQuery(Guid Id) : IRequest<EmployeesModel>;

public class GetEmployeeByIdQueryHandler : BaseQueryHandler, IRequestHandler<GetEmployeeByIdQuery, EmployeesModel>
{
  public GetEmployeeByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

  public async Task<EmployeesModel> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
  {
    var employee = await UnitOfWork.Repository<EmployeeEntity>().GetAsync(e => e.Id == request.Id, query => query.Include(x => x.Department)); 
    return Mapper.Map<EmployeesModel>(employee.FirstOrDefault());
  }
}
