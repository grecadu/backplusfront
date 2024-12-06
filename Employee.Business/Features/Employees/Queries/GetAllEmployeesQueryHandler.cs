using AutoMapper;
using Employee.Business.Common;
using Employee.Business.Interfaces;
using Employee.Business.Models;
using Employee.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Business.Features.Employees.Queries;

public record GetAllEmployeesQuery : IRequest<IEnumerable<EmployeesModel>>;


public class GetAllEmployeesQueryHandler : BaseQueryHandler, IRequestHandler<GetAllEmployeesQuery, IEnumerable<EmployeesModel>>
{

  public GetAllEmployeesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }

  public async Task<IEnumerable<EmployeesModel>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
  {

    var employees = await UnitOfWork.Repository<EmployeeEntity>().GetAsync(e=> true, query => query.Include(x=> x.Department));

    return Mapper.Map<IEnumerable<EmployeeEntity>, IEnumerable<EmployeesModel>>(employees);


  }
}
