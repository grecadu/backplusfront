using AutoMapper;
using Employee.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Business.Common;

public class BaseCommandHandler
{

  protected readonly IUnitOfWork UnitOfWork;
  protected readonly IMapper Mapper;
  public BaseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
  {
    UnitOfWork = unitOfWork;
    Mapper = mapper;
  }
}
