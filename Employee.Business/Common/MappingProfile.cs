using AutoMapper;
using Employee.Business.Models;
using Employee.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Business.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
    CreateMap<EmployeeEntity, EmployeesModel>(MemberList.None);
    CreateMap<EmployeesModel, EmployeeEntity>(MemberList.None);

    CreateMap<DepartmentEntity, DepartmentModel>(MemberList.None);
    CreateMap<DepartmentModel, DepartmentEntity>(MemberList.None);
  }
}
