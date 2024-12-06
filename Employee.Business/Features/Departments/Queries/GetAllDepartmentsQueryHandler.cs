using AutoMapper;
using Employee.Business.Common;
using Employee.Business.Features.Employees.Queries;
using Employee.Business.Interfaces;
using Employee.Business.Models;
using Employee.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Employee.Business.Features.Departments.Queries;

public record GetAllDepartmentsQuery : IRequest<IEnumerable<DepartmentModel>>;

public class GetAllDepartmentsQueryHandler : BaseQueryHandler, IRequestHandler<GetAllDepartmentsQuery, IEnumerable<DepartmentModel>>
{
  public GetAllDepartmentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
  {
  }

  public async Task<IEnumerable<DepartmentModel>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
  {

    var departments = await UnitOfWork.Repository<DepartmentEntity>().GetAllAsync();

    return Mapper.Map<IEnumerable<DepartmentEntity>, IEnumerable<DepartmentModel>>(departments);


  }
}
